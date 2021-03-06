﻿using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Command.Groups;
using FWCB2014.Domain.Core.Services;
using FWCB2014.Import.Core.Services;
using FWCB2014.Import.Infrastructure.Services;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Linq;
using System.Xml.Linq;

namespace FWCB2014.Import.Infrastructure.Tests.Services
{
  [TestFixture]
  public class XmlGroupsService_Tests
  {
    private IGroupsService<GroupModel<TeamModel>> _service;

    [SetUp]
    public void Given_An_XmlGroupsService()
    {
      var feed = XElement.Load(@"C:\Users\mattia.richetto\Dropbox\dotNet\prj\FWCB2014\FWCB2014.Application.Web\App_Data\Standings_20140405.xml");
      _service = new XmlGroupsService(feed);
    }

    [Test]
    public void It_Should_Be_Able_To_Return_8_Groups()
    {
      var groups = _service.GetAll();

      var json = JsonConvert.SerializeObject(groups);

      Assert.AreEqual(8, groups.Count());
    }

    [Test]
    public void The_Name_Of_The_4th_Group_Is_Group_D()
    {
      var groups = _service.GetAll();

      Assert.AreEqual("Group D", groups.Skip(3).Take(1).First().Name);
    }

    [Test]
    public void Every_Group_Has_4_Teams()
    {
      var groups = _service.GetAll();

      Assert.AreEqual(4, groups.First().Teams.Count());
    }

    [Test]
    public void The_Name_Of_The_2nd_Team_In_The_4th_Group_Is_England()
    {
      var groups = _service.GetAll();

      var group = groups.Skip(3).Take(1).First();

      Assert.AreEqual("eng_int", group.Teams.Skip(1).Take(1).First().Code);
    }
  }
}
