using System;
using System.Collections.Generic;
using ArtifactTypeConverter.Tests.Helpers;
using kCura.Relativity.Client;
using NUnit.Framework;

namespace ArtifactTypeConverter.Tests
{
    [TestFixture]
    public class UnitTest1
    {
        [TestCase]
        [Description("Test creating an artifact to be used for testing")]
        public void CreateArtifactTest()
        {
            //Create Artifact
            var artifact = ArtifactHelper.CreateTestArtifact();

            Assert.That(artifact, Is.Not.Null);
        }
    }
}
