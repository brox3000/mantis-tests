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

           if(!app.prMH.IsElementPresent(app.prMH.Project_MH))

            {
                Random rnd = new Random();
                int value = rnd.Next();

                ProjectData projectData = new ProjectData("first" + value, "descFirst");
                app.prMH.Add(projectData);
            }

            List<ProjectData> OldPjs = app.prMH.GetProjectsList();
            app.prMH.RemoveProject();

            List<ProjectData> NewPjs = app.prMH.GetProjectsList();
            List<ProjectData> n_remove = OldPjs.Except(NewPjs).ToList();

            ProjectData toBeRemoved = n_remove.First();

            OldPjs.Remove(toBeRemoved);
            OldPjs.Sort();
            NewPjs.Sort();

            Assert.AreEqual(OldPjs, NewPjs);
            Assert.AreEqual(OldPjs.Count, NewPjs.Count);
        }
    }
}
