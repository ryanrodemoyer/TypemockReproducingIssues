# Faked Instance Method Default Return Type

Given the scenario:

- A faked method that returns an `enum` value.

I assert that the value should equal `default(EnumType)`.

In version 8.6.1.0 - this was the behavior.

Starting at (perhaps earlier) version 8.6.10.3, this behavior changed. The behavior is now to return a value that is equal to the lowest non-negative member of the enum.

## Reproducing

In any of the test projects, please see the `FakedEnumTests.cs` file.

- The `ColorConverterTests.DefaultValueForEnumIs0` test shows what I believe should be the correct/desired/target behavior. A faked instance should have a return value equal to the `default(Type)` for that return.
- The `ColorConverterTests.FakedInstance_MethodThatReturnsEnumReturnsNonDefaultValue` a test that:
   - passes in version 8.6.1.0.
   - fails in versions starting (no later than) 8.6.10.3.

# Reproducing Stale Fakes

Either test passes individually.

One test fails when running all tests together.

`[Isolated]` attribute exists in the `AssemblyInfo.cs` file.

## Help me out! How am I supposed to fake singletons when running in different test methods?

One of these two test methods will fail and produce a warning about stale fakes. I'm lost on how to either a) resolve this error or b) test this in a way that's acceptable for Typemock.

## Reproduced with

1. Visual Studio 2017.
1. Windows 10

The test runner is any of Typemock Test Navigator, Resharper or VS Test Explorer.