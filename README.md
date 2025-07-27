## Finance Example in C#: Black–Scholes Pricing

This C# project provides a simple implementation of the Black–Scholes
model for pricing European call and put options and computing their
Delta (sensitivity to the underlying asset). The code is self‑contained
and can be compiled with a C# compiler (e.g., `csc` or `dotnet`)
without external dependencies.

### Features

* Functions to compute call price, put price, and Delta for call and
  put options.
* Command‑line interface allows users to supply parameters such as
  underlying price, strike price, risk‑free rate, volatility and time
  to maturity.
* Provides an example in the `Main` method demonstrating default
  parameter usage.

### Building

To build and run using the .NET CLI:

```sh
dotnet new console -n FinanceExample
cp BlackScholes.cs FinanceExample/Program.cs
cd FinanceExample
dotnet run -- 100 100 0.05 0.2 1
```

Alternatively, compile directly with the C# compiler:

```sh
csc BlackScholes.cs
mono BlackScholes.exe 100 100 0.05 0.2 1
```

### Further work

This example illustrates how to implement basic quantitative finance
calculations in C#. For more advanced applications—such as pricing
exotic options, building yield curves or performing risk
management—developers may prefer to use a comprehensive library like
QuantLib. QuantLib is a free open‑source library written in C++ and
exported to C#, Java, Python and other languages【574555413661620†L40-L48】. It
provides tools for modeling, trading and risk management that are used
in industry and academia【574555413661620†L40-L48】.
