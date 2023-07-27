using ClassLibrary1;
using ClassLibrary1.Abstractions;
using Moq;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DirTest.Tests
{
    public class LocalisationMenegerTests
    {
        [Fact]
        public void AddingSourseTest()
        {
            var stringCode = 123;
            var culture = new CultureInfo(1);
            var testCase = "testCase";
            Thread.CurrentThread.CurrentCulture = culture;
            var sut = new LocalizationManager();
            var stub = new Mock<ILocalisationStringSource>();
            stub.Setup(x => x.GetStringByCodeAndCultural(stringCode, culture)).Returns(testCase);
            

            var resultWithoutSource = sut.GetString(stringCode, culture);
            sut.RegisterSource(stub.Object);
            var resultWithSourse = sut.GetString(stringCode, culture);

            Assert.True(testCase == resultWithSourse && resultWithSourse != resultWithoutSource);
        }
        [Fact]
        public void ManagerReturnNullWithoutStrings()
        {
            var stringCode = 123;
            var culture = new CultureInfo(1);
            string testCase = null;
            Thread.CurrentThread.CurrentCulture = culture;
            var sut = new LocalizationManager();
            var stub = new Mock<ILocalisationStringSource>();
            stub.Setup(x => x.GetStringByCodeAndCultural(stringCode, culture)).Returns(testCase);
            sut.RegisterSource(stub.Object);

            var result = sut.GetString(stringCode, culture);

            Assert.True(testCase == result);
        }
        [Fact]
        public void ManagerReturnStringFromDataSourse()
        {
            var stringCode = 123;
            var culture = new CultureInfo(1);
            var testCase = "testCase";
            Thread.CurrentThread.CurrentCulture = culture;
            var sut = new LocalizationManager();
            var stub = new Mock<ILocalisationStringSource>();
            stub.Setup(x => x.GetStringByCodeAndCultural(stringCode, culture)).Returns(testCase);
            sut.RegisterSource(stub.Object);

            var result = sut.GetString(stringCode, culture);

            Assert.True(testCase == result);
        }
        [Fact]
        public void ManagerWorkWithThreadCulture()
        {
            var stringCode = 123;
            var culture = new CultureInfo(1);
            var testCase = "testCase";
            Thread.CurrentThread.CurrentCulture = culture;
            var sut = new LocalizationManager();
            var stub = new Mock<ILocalisationStringSource>();
            stub.Setup(x => x.GetStringByCodeAndCultural(stringCode, culture)).Returns(testCase);
            sut.RegisterSource(stub.Object);

            var resultWithoutCulture = sut.GetString(stringCode, null);

             Assert.True(testCase == resultWithoutCulture);
        }
        [Fact]
        public void ManagerWorkWithManyContainersWithStrings()
        {
            var stringCode = 123;
            var culture = new CultureInfo(1);
            var testCase1 = "testCase3";
            var testCase2 = "testCase21";
            var sut = new LocalizationManager();
            var FirstStub = new Mock<ILocalisationStringSource>();
            FirstStub.Setup(x => x.GetStringByCodeAndCultural(stringCode, culture)).Returns(testCase1);
            var SecondStub = new Mock<ILocalisationStringSource>();
            SecondStub.Setup(x => x.GetStringByCodeAndCultural(stringCode, culture)).Returns(testCase2);

            sut.RegisterSource(FirstStub.Object);
            sut.RegisterSource(SecondStub.Object);
            var resultWithSourse = sut.GetString(stringCode, culture);

            Assert.True(testCase1 == resultWithSourse || resultWithSourse != testCase2);
        }
        [Fact]
        public void ManagerWorkWithManyContainers()
        {
            var stringCode = 123;
            var culture = new CultureInfo(1);
            string testCase1 = "testCase3";
            string testCase2 = null;
            var sut = new LocalizationManager();
            var FirstStub = new Mock<ILocalisationStringSource>();
            FirstStub.Setup(x => x.GetStringByCodeAndCultural(stringCode, culture)).Returns(testCase1);
            var SecondStub = new Mock<ILocalisationStringSource>();
            SecondStub.Setup(x => x.GetStringByCodeAndCultural(stringCode, culture)).Returns(testCase2);

            sut.RegisterSource(FirstStub.Object);
            sut.RegisterSource(SecondStub.Object);
            var resultWithSourse = sut.GetString(stringCode, culture);

            Assert.True(testCase1 == resultWithSourse);
        }
    }
}