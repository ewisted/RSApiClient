using NUnit.Framework;
using RSApiClient.Extensions;
using RSApiClient.Hiscores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSApiClient.UnitTests.ExtensionTests
{
	public class HiscoreExtensionTests
	{
		[Test]
		public void GetModuleStringValueTest()
		{
			Assert.AreEqual("hiscore", HiscoreType.RS3Normal.GetModuleStringValue());
			Assert.AreEqual("hiscore_ironman", HiscoreType.RS3Ironman.GetModuleStringValue());
			Assert.AreEqual("hiscore_hardcore_ironman", HiscoreType.RS3HardcoreIronman.GetModuleStringValue());
			Assert.AreEqual("hiscore_oldschool", HiscoreType.OSNormal.GetModuleStringValue());
			Assert.AreEqual("hiscore_oldschool_ironman", HiscoreType.OSIronman.GetModuleStringValue());
			Assert.AreEqual("hiscore_oldschool_hardcore_ironman", HiscoreType.OSHarcoreIronman.GetModuleStringValue());
			Assert.AreEqual("hiscore_oldschool_ultimate", HiscoreType.OSUltimateIronman.GetModuleStringValue());
			Assert.AreEqual("hiscore_oldschool_deadman", HiscoreType.OSDeamanMode.GetModuleStringValue());
			Assert.AreEqual("hiscore_oldschool_seasonal", HiscoreType.OSSeasonal.GetModuleStringValue());
			Assert.AreEqual("hiscore_oldschool_tournament", HiscoreType.OSTournament.GetModuleStringValue());
			Assert.Throws<ArgumentOutOfRangeException>(() => ((HiscoreType)10).GetModuleStringValue());
		}

		[Test]
		public void GetNameStringTest()
		{
			Assert.AreEqual("April Fools 2015 - Rats Killed After Miniquest", RS3HiscoresCsvMapping.AprilFools2015_RatsKilledAfterMiniquest.GetNameString());
			Assert.AreEqual("Cabbage Facepunch Bonanza - 5 Game Average", RS3HiscoresCsvMapping.CabbageFacepunchBonanza_5GameAverage.GetNameString());
			Assert.Throws<ArgumentOutOfRangeException>(() => ((HiscoreType)10).GetNameString());
		}
	}
}
