using Microsoft.Extensions.FileProviders;
using DABTechs.eCommerce.Sales.Providers.Menu.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DABTechs.eCommerce.Sales.Providers.Menu.Mapper
{
    // TODO the xml parsing is flakey done like this for time constraints need refactoring
    public class MegaNavMapper
    {
        public MegaNavMapper()
        {

        }

        public async Task<MegaNav> Map(IFileInfo file)
        {
            var meganav = new MegaNav();

            // get content from xml file
            using (var stream = file.CreateReadStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var contents = await reader.ReadToEndAsync();
                    meganav = MapToMegaNav(contents);
                }
            }

            return meganav;
        }

        private MegaNav MapToMegaNav(string contents)
        {
            var megaNav = new MegaNav();
            var xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(contents);

            XmlNode root = xmlDoc.DocumentElement;

            var departments = root.SelectNodes("*");

            foreach (XmlNode dep in departments)
            {
                var name = ((XmlElement)dep).GetAttribute("name");
                var siteUrl = ((XmlElement)dep).GetAttribute("siteurl");
                var searchLinks = GetSearchLinks(dep.SelectNodes("*"));
                megaNav.Departments.Add(new Department { Name = name, SiteUrl = siteUrl, SearchLinks = searchLinks });
            }

            // This should have worked but didn't needs tro be revisited
            //var serializer = new XmlSerializer(typeof(MegaNav));
            //var stringReader = new StringReader(contents);
            //return (MegaNav)serializer.Deserialize(stringReader);

            return megaNav;
        }

        private List<SearchLink> GetSearchLinks(XmlNodeList searchLinks)
        {
            var linksList = new List<SearchLink>();

            foreach (XmlNode links in searchLinks)
            {
                var columns = GetColumns(links.SelectNodes("*"));
                linksList.Add(new SearchLink { Columns = columns });
            }

            return linksList;
        }

        private List<Column> GetColumns(XmlNodeList columns)
        {
            var columnsList = new List<Column>();

            foreach (XmlNode col in columns)
            {
                var sections = GetSections(col.SelectNodes("*"));
                columnsList.Add(new Column { Sections = sections });
            }

            return columnsList;
        }

        private List<Section> GetSections(XmlNodeList sections)
        {
            var sectionsList = new List<Section>();

            foreach (XmlNode sec in sections)
            {
                var title = ((XmlElement)sec).GetAttribute("title");
                var links = GetLinks(sec.SelectNodes("*"));
                sectionsList.Add(new Section { Title = title, Links = links });
            }

            return sectionsList;
        }

        private List<Link> GetLinks(XmlNodeList links)
        {
            var linksList = new List<Link>();

            foreach (var link in links)
            {
                var title = ((XmlElement)link).GetAttribute("title");
                var siteurl = ((XmlElement)link).GetAttribute("siteurl");
                var newin = ((XmlElement)link).GetAttribute("newin");

                linksList.Add(new Link { Title = title, SiteUrl = siteurl, NewIn = newin });
            }

            return linksList;
        }
    }
}
