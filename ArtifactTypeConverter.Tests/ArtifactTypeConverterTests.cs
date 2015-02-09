using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ArtifactTypeConverter.Tests.Helpers;
using ArtifactTypeConverter.Tests.Models;
using kCura.Relativity.Client;
using NUnit.Framework;

namespace ArtifactTypeConverter.Tests
{
    [TestFixture]
    public class ArtifactTypeConverterTests
    {
        private ArtifactTypeConverter _artifactConverter;

        private Artifact testArtifact;

        [SetUp]
        public void Setup()
        {
            _artifactConverter = new ArtifactTypeConverter();

            //Setup artifact
            testArtifact = ArtifactHelper.CreateTestArtifact();
        }

        #region FieldConvertingTests

        [Test]
        public void GetFieldValue_StringSuccessTest()
        {
            var field = new Field { Value = ByteArrayHelper.GetBytes("Testing this out") };

            var result = _artifactConverter.GetFieldValue<string>(field);

            Assert.That(result, Is.EqualTo("Testing this out"));
        }

        [Test]
        public void GetFieldValue_EnumSuccessTest()
        {
            var field = new Field { Value = ByteArrayHelper.GetBytes("Monday") };

            var result = _artifactConverter.GetFieldValue<DayOfWeek>(field);

            Assert.That(result, Is.EqualTo(DayOfWeek.Monday));
        }

        [Test]
        public void GetFieldValue_NullableEnumSuccessTest()
        {
            var field = new Field { Value = ByteArrayHelper.GetBytes("Monday") };

            var result = _artifactConverter.GetFieldValue<DayOfWeek?>(field);

            Assert.That(result, Is.EqualTo(DayOfWeek.Monday));
        }

        [Test]
        public void GetFieldValue_IntSuccessTest()
        {
            var field = new Field { Value = 123 };

            var result = _artifactConverter.GetFieldValue<int>(field);

            Assert.That(result, Is.EqualTo(123));
        }

        [Test]
        public void GetFieldValue_NullableIntSuccessTest()
        {
            var field = new Field { Value = 1234 };

            var result = _artifactConverter.GetFieldValue<int?>(field);

            Assert.That(result, Is.EqualTo(1234));
        }

        [Test]
        public void GetFieldValue_NullableIntIsNullSuccessTest()
        {
            var field = new Field { Value = null };

            var result = _artifactConverter.GetFieldValue<int?>(field);

            Assert.That(result, Is.Null);
        }

        #endregion

        #region ArtifactConvertionTests

        [Test]
        public void ArtifactConvertion_SuccessTest()
        {
            var result = _artifactConverter.Convert<TestRdo>(testArtifact);

            Assert.That(result.ImportObjectType, Is.EqualTo(12345));
            Assert.That(result.Login, Is.EqualTo("relativity.admin"));
            Assert.That(result.DayOfTheWeek, Is.EqualTo(DayOfWeek.Monday));
            Assert.That(result.DontMap, Is.Null, "DontMap Field should have been skipped.");
        }

        [Test]
        public void MultiArtifactConversion_SuccessTest()
        {
            var artifactList = new List<Artifact>();

            for (int i = 0; i < 1000; i++)
            {
                artifactList.Add(ArtifactHelper.CreateTestArtifact());
            }

            var resultList = _artifactConverter.Convert<TestRdo>(artifactList);

            Assert.That(resultList.Count(), Is.EqualTo(artifactList.Count));
        }

        #endregion

        #region PerformanceTests

        [Test]
        [Explicit]
        public void MultiConvertionPerfTest_OneMillion()
        {
            //Setup
            var artifactList = new List<Artifact>();

            for (int i = 0; i < 1000000; i++)
            {
                artifactList.Add(ArtifactHelper.CreateTestArtifact());
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            _artifactConverter.Convert<TestRdo>(artifactList);
            stopwatch.Stop();

            Console.WriteLine("Mapped 1 million artifacts in {0} seconds", stopwatch.Elapsed.TotalSeconds);
        }

        #endregion
    }
}
