# BloxSharp

> _An experimental C# to Luau transpiler for Roblox, built by someone who absolutely has not done this before but figured "hey, how hard could it be?"_

---

## What Is This?

**BloxSharp** is an ongoing experiment to make Roblox game development suck a little less — at least if you're the kind of masochist who enjoys writing C# more than fiddling with nested Luau tables and pretending `:WaitForChild()` is a dependency injection strategy.

This project lets you:

- Write Roblox game logic in **real C#**
- Annotate your code with attributes like `[LuauClass]` and `[LuauMethod]`
- Magically (and probably incorrectly) convert that code into Luau
- Feel powerful... until you realize you still have to test everything in Studio

---

## But Seriously... What's It Do?

BloxSharp walks your decorated C# classes and:

1. **Uses reflection and Roslyn** to extract structure and method bodies
2. Builds an **intermediate representation** (IR) — because that's what serious compilers do
3. Emits vaguely passable **Luau code** — because that's what Roblox demands
4. Writes the results to `.lua` files so you can drop them into your project and pretend it’s normal

Is it production-ready?  
**Absolutely not.** This project is held together by reflection, caffeine, and sarcasm.



---

## Sample C# Input

```csharp
[LuauClass("Person")]
public class Person
{
    [LuauField] public string Name { get; set; }
    [LuauField] public int Age { get; set; }

    [LuauMethod]
    public void Greet()
    {
        if (Age >= 18)
            Console.WriteLine("Hello, adult!");
        else
            Console.WriteLine("I'm just a kid!");
    }
}
```

## Sample Output Luau

```lua
local Person = {}
Person.__index = Person

function Person.new(name, age)
    local self = setmetatable({}, Person)
    self.Name = name
    self.Age = age
    return self
end

function Person:Greet()
    if self.Age >= 18 then
        print("Hello, adult!")
    else
        print("I'm just a kid!")
    end
end

return Person
```

---

## Roadmap 🛣️

Because ambition is free and sarcasm is eternal:

- [x] C# class → Luau module transpilation (basic)
- [x] Method body translation (return, if, basic calls)
- [x] `Console.WriteLine` becomes `print` like magic
- [x] Recursive `.cs` file detection (like a bloodhound with ADHD)
- [ ] 🚧 Automated compilation pipeline for bulk class emission
- [ ] 🚧 Support for native Roblox services (`game:GetService("Whatever")`)
- [ ] 🚧 Roblox event wiring (Connect, Disconnect, OnTouched, etc.)
- [ ] 🚧 Rudimentary game framework with lifecycle support (Init, Tick, etc.)
- [ ] 🚧 Live sync to your Roblox Studio environment (and maybe coffee delivery)
- [ ] 🚧 Type system expansion (eventually... probably... maybe)



## What Works

- ✅ Attributes `[LuauClass]`, `[LuauField]`, `[LuauMethod]`
- ✅ Simple method translation (including `if`, `return`, method calls)
- ✅ Basic method call resolution (`self:Something()`)
- ✅ Runtime file location for source parsing

## What Doesn't (Yet)

- ❌ Inheritance or advanced types
- ❌ Static class translation
- ❌ Actual project integration
- ❌ Multiverse travel or emotional support

---

## How To Use

```csharp
var locator = new DefaultSourceLocator(DefaultSourceLocator.GetProjectRoot());
var builder = new ReflectionIRBuilder(
    new LuauStatementTranslator(new LuauExpressionTranslator()),
    locator
);
var emitter = new LuauCodeEmitter();
var engine = new CompilationEngine(builder, emitter);
engine.Compile(typeof(Person), "Person.lua");
```

---
## Legal & Emotional Disclaimers

- This is not officially supported by Roblox.
- This is not officially supported by me.
- I am not a compiler engineer.
- I have never built a Luau transpiler before.
- Honestly, I’m still surprised it compiles anything at all.
- If it breaks your game, your machine, or your spirit — that's on you.

---

## Contributing
Sure, go nuts. Just don’t submit PRs that make it use XML comments or Java-style enums. I have standards.

---

## License

MIT, because if you’re brave enough to build games with this, you deserve some legal protection.

---

## Closing Thoughts

_“Code like nobody's watching. Then throw it in the compiler and regret everything.”

---

_This project is experimental. I make no guarantees. I've never built a Luau transpiler before. You probably shouldn't use this. But here you are._
