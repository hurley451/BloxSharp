using BloxSharp.Compiler.Attributes;


namespace Samples
{
    [LuauClass("Person")]
    public class Person
    {
        [LuauField] public string Name { get; set; } = string.Empty;
        [LuauField] public int Age { get; set; }

        [LuauMethod]
        public void Introduce()
        {
            Console.WriteLine("Hello, my name is " + Name);
        }

        [LuauMethod]
        public void Greet()
        {
            if (Age >= 18)
                Introduce();
            else
                Console.WriteLine("I'm just a kid!");
        }
    }
}