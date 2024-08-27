using csv2object;
namespace test_csv2object
{
    public class test_csvToObject
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void nUnitTest()
        {
            Assert.Pass();
        }

        [Test]
        public void singleLineExample() 
        {
            string csv_example = "name;age;town\n" +
                                 "Wesley Peters;29;London";
            var list = csv2object.csv2object.Convert<ExampleClass1>(csv_example, ';');
            
            ExampleClass1 correctObject = new ExampleClass1
            {
                Name = "Wesley Peters",
                Age = 29,
                Town = "London"
            };
            List<ExampleClass1> correctList = new List<ExampleClass1> {correctObject};

            Assert.That(list, Is.EqualTo(correctList));
        }
    }
}