using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using HtmlAgilityPack;
using Models;

namespace Services
{
    public class TibiaParser
    {
        public TibiaCharacter GetTibiaCharacter(string charName)
        {
            string formattedName = charName.Replace(" ", "+");
            string path = @"https://secure.tibia.com/community/?subtopic=characters&name=" + formattedName;
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(path);

            TibiaCharacter tibiaCharacter = new TibiaCharacter();
            tibiaCharacter.Name = charName;

            foreach(HtmlNode row in htmlDoc.DocumentNode.SelectNodes("//div[@id=\"characters\"]//table/tr/td"))
            {
                string rowTitle = row.InnerText;
                switch(rowTitle)
                {
                    case "Vocation:":
                        tibiaCharacter.Vocation = row.NextSibling.InnerText;
                        break;
                    case "Level:":
                        tibiaCharacter.Level = Int32.Parse(row.NextSibling.InnerText);
                        break;
                    case "World:":
                        tibiaCharacter.World = row.NextSibling.InnerText;
                        break;
                    case "Residence:":
                        tibiaCharacter.Residence = row.NextSibling.InnerText;
                        break;
                    case "Guild&#160;Membership:":
                        tibiaCharacter.GuildName = row.NextSibling.InnerText;
                        break;
                    case "Last Login:":
                        string[] lastLoginSplitted = row.NextSibling.InnerText.Split(new string[] { "&#160;" }, StringSplitOptions.None);
                        string year = lastLoginSplitted[2].Substring(0, lastLoginSplitted[2].Length - 1);
                        DateTime dt = DateTime.ParseExact(year + "-" + lastLoginSplitted[0] + "-" + lastLoginSplitted[1] + " " + lastLoginSplitted[3],
                            "yyyy-MMM-dd HH:mm:ss",
                            CultureInfo.InvariantCulture);
                        tibiaCharacter.LastLogin = dt;
                        break;



                }
            }

            return tibiaCharacter;
        }


        public List<TibiaCharacter> GetGuildCharacters(string guildName)
        {
            string formattedGuildName = guildName.Replace("\u00A0", "+");
            string path = @"https://secure.tibia.com/community/?subtopic=guilds&page=view&GuildName=" + formattedGuildName;

            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(path);

            List<TibiaCharacter> characters = new List<TibiaCharacter>();

            foreach(HtmlNode row in htmlDoc.DocumentNode.SelectNodes("//table[@class='Table3']//tr[@bgcolor='#F1E0C6']/td//a"))  
            {
                string characterName = row.InnerText.Replace("&#160;", " ");
                TibiaCharacter character = GetTibiaCharacter(characterName);
                characters.Add(character);
            }


            return characters;
        }

        public List<TibiaCharacter> GetOnlineCharacters(string worldName)
        {
            string path = "https://secure.tibia.com/community/?subtopic=worlds&world=" + worldName;

            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(path);

            List<TibiaCharacter> characters = new List<TibiaCharacter>();

            foreach (HtmlNode row in htmlDoc.DocumentNode.SelectNodes("//table[@class='Table2']//tr[@class='Odd' or @class='Even']/td//a[not(@name)]"))
            {
                string characterName = row.InnerText.Replace("&#160;", " ");
                TibiaCharacter character = GetTibiaCharacter(characterName);
                characters.Add(character);
            }

            return characters;
        }

    }
}
