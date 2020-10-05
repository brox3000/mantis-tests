using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Linq;

namespace mantis_tests
{
    [TestFixture]
    public class RemoveProject:AuthTestBase
    {
        [Test]
		
        public void RemoveProjectMantis()
        {
            app.Navigator.OpenFromMenu();
            app.Navigator.OpenProjectsMenu();
			List<ProjectData> Newlist = new List<ProjectData>();
            List<ProjectData> Oldlist = new List<ProjectData>();
            AccountData account = new AccountData("administrator", "2020");
			
           if(!app.prMH.IsElementPresent(app.prMH.Project_MH))
            {
                Random rnd = new Random();
                int value = rnd.Next();

                 //rojectData projectData = new ProjectData("first" + value, "descFirst");
                ProjectData project = new ProjectData("Kasin_add_delete_" + value, "The End Delete");
                //app.prMH.Add(projectData);
				app.prMH.CreateNewProjectFromAPI(account, project);													 
            }

            List<Mantis.ProjectData> OldProjects = app.prMH.GetProjectsListFromApi(account);
			
            for (int i = 0; i < OldProjects.Count; i++)
            {
                Oldlist.Add(new ProjectData(OldProjects[i].name, OldProjects[i].description));
            }

            app.Navigator.OpenFromMenu();
            app.Navigator.OpenProjectsMenu();
            app.prMH.RemoveProject();

            List<Mantis.ProjectData> NewProjects = app.prMH.GetProjectsListFromApi(account);

            for (int i = 0; i < NewProjects.Count; i++)
            {
                Newlist.Add(new ProjectData(NewProjects[i].name, NewProjects[i].description));
            }

            List<ProjectData> n_remove = Oldlist.Except(Newlist).ToList();
            ProjectData toBeRemoved = n_remove.First();

            Oldlist.Remove(toBeRemoved);

            Assert.AreEqual(Oldlist.Count, Newlist.Count);

            Oldlist.Sort();
            Newlist.Sort();

            Assert.AreEqual(Oldlist,Newlist);
        }
    }
}
