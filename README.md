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