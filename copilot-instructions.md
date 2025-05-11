# Copilot Instructions for BloxSharp (C# to Luau Transpiler)

Welcome to the BloxSharp transpiler codebase. This project is designed to convert annotated C# classes into Roblox-compatible Luau module scripts. Use these guidelines to help Copilot (or other AI agents) generate aligned and maintainable code.

---

## 🔧 Project Purpose

BloxSharp is a **C# to Luau transpiler**. It converts .NET-style class declarations into Roblox Luau module scripts. It supports:

- Class, method, and field annotations via attributes
- Roslyn-powered source analysis
- Luau-compatible syntax and structure generation
- DI-driven architecture for builders, emitters, and translators

---

## 🧱 Code Structure Overview

- `Interfaces/` — Defines core abstractions (`IIRBuilder`, `ICodeEmitter`, `IStatementTranslator`, `IExpressionTranslator`)
- `Emitters/` — Classes that generate code output (e.g., `LuauCodeEmitter`)
- `Translators/` — Convert Roslyn syntax nodes to Luau source code
- `IR/` — Defines `IRClass`, `IRField`, and `IRMethod` for intermediate representation

---

## 🗂 File Naming Conventions

- Classes implementing interfaces use suffixes like `LuauCodeEmitter`, `LuauStatementTranslator`
- Interface filenames should match the interface name exactly (e.g., `IIRBuilder.cs`)
- Output emitters should live in the `Emitters/` folder and follow the target language naming (`Luau`, `Lua51`, etc.)

---

## ✅ Style and Function Expectations

### When writing translators:
- `Translate()` methods should **never emit full syntax trees** — return **flat Luau strings**
- Translate `this.X` to `self.X`
- Always return `"-- TODO"` for unrecognized syntax nodes

### When writing emitters:
- Use `StringBuilder` and proper indentation
- Generate constructor functions and instance methods
- Ensure methods with no body emit a `"-- TODO"` comment

---

## 📌 Attributes Used

```csharp
[LuauClass("ClassName")]        // Marks a class for Luau conversion
[LuauField]                     // Marks a property or field for inclusion
[LuauMethod]                    // Marks a method to be emitted
[LuauGlobal("game.Workspace")]  // Binds a class/method to a Luau global
[LuauNamespace("game.Services")]// Emits class inside a Luau service tree
[LuauYield]                     // Marks method as yielding (future use)
[LuauIgnore]                    // Omits field/method from output
```

---

## 🧪 Testing Philosophy

Tests are performed via:
- Sample annotated C# classes (`Person`, `Enemy`, etc.)
- Direct comparison of emitted `.lua` files to expected output
- Use of `luau-analyze` for syntax/type correctness (TBD integration)

---

## 🤖 Copilot Prompting Tips

When using GitHub Copilot:
- Prompt with “Implement a Luau emitter for...” or “Translate this IfStatementSyntax...”
- Reference the interfaces (`IExpressionTranslator`) and prefer injection over static usage
- Be explicit about what type of node you're working with (e.g., `BinaryExpressionSyntax`)

---

## 📎 Example Usage

```csharp
var builder = new ReflectionIRBuilder(new LuauStatementTranslator(new LuauExpressionTranslator()));
var emitter = new LuauCodeEmitter();
var engine = new CompilationEngine(builder, emitter);
engine.Compile(typeof(Sample.Person), "Person.lua");
```

---

Happy coding! 🚀