/*
 * Black–Scholes option pricing model implementation in C#
 *
 * This project demonstrates a simple financial calculation: pricing
 * European call and put options using the Black–Scholes formula. The
 * program defines a static class with methods for computing the value of
 * a call option and a put option, as well as the Delta (sensitivity of
 * option price to changes in the underlying asset). Users can run the
 * program and supply input parameters via the command line. For more
 * advanced quantitative finance capabilities, consider using a
 * dedicated library like QuantLib, which is written in C++ and
 * exported to C# and other languages for comprehensive modeling,
 * trading and risk management【574555413661620†L40-L47】. QuantLib provides a
 * complete framework and is widely used in the finance industry【574555413661620†L40-L48】.
 */

using System;

namespace Finance
{
    public static class BlackScholes
    {
        // Standard normal cumulative distribution function
        private static double N(double x)
        {
            return 0.5 * (1.0 + Math.Erf(x / Math.Sqrt(2.0)));
        }

        // Compute d1 and d2 terms for Black–Scholes formula
        private static (double d1, double d2) ComputeD1D2(double S, double K, double r, double sigma, double T)
        {
            double numerator = Math.Log(S / K) + (r + 0.5 * sigma * sigma) * T;
            double denominator = sigma * Math.Sqrt(T);
            double d1 = numerator / denominator;
            double d2 = d1 - sigma * Math.Sqrt(T);
            return (d1, d2);
        }

        // Price of a European call option
        public static double CallPrice(double S, double K, double r, double sigma, double T)
        {
            var (d1, d2) = ComputeD1D2(S, K, r, sigma, T);
            double call = S * N(d1) - K * Math.Exp(-r * T) * N(d2);
            return call;
        }

        // Price of a European put option
        public static double PutPrice(double S, double K, double r, double sigma, double T)
        {
            var (d1, d2) = ComputeD1D2(S, K, r, sigma, T);
            double put = K * Math.Exp(-r * T) * N(-d2) - S * N(-d1);
            return put;
        }

        // Delta of a European call option
        public static double CallDelta(double S, double K, double r, double sigma, double T)
        {
            var (d1, _) = ComputeD1D2(S, K, r, sigma, T);
            return N(d1);
        }

        // Delta of a European put option
        public static double PutDelta(double S, double K, double r, double sigma, double T)
        {
            var (d1, _) = ComputeD1D2(S, K, r, sigma, T);
            return N(d1) - 1.0;
        }

        // Example usage in main program
        public static void Main(string[] args)
        {
            // Example parameters (S: underlying price, K: strike price,
            // r: risk‑free rate, sigma: volatility, T: time to maturity in years)
            double S = 100.0;
            double K = 100.0;
            double r = 0.05;      // 5% annual risk‑free rate
            double sigma = 0.2;    // 20% annual volatility
            double T = 1.0;        // 1 year until expiration

            if (args.Length >= 5)
            {
                // Parse user‑provided parameters if supplied
                double.TryParse(args[0], out S);
                double.TryParse(args[1], out K);
                double.TryParse(args[2], out r);
                double.TryParse(args[3], out sigma);
                double.TryParse(args[4], out T);
            }

            // Instantiate EuropeanOption objects using OOP design
            var callOption = new EuropeanOption(S, K, r, sigma, T, true);
            var putOption = new EuropeanOption(S, K, r, sigma, T, false);

            double call = callOption.Price();
            double put = putOption.Price();
            double callDelta = callOption.Delta();
            double putDelta = putOption.Delta();

            Console.WriteLine($"Call Price: {call:F4}");
            Console.WriteLine($"Put Price: {put:F4}");
            Console.WriteLine($"Call Delta: {callDelta:F4}");
            Console.WriteLine($"Put Delta: {putDelta:F4}");
        }
    }

    /// <summary>
    /// Represents a European option (call or put) priced using the
    /// Black–Scholes model. Encapsulates the option’s parameters and
    /// provides methods to compute price and delta. This object
    /// follows OOP principles by modeling an option with state and
    /// behavior.
    /// </summary>
    public class EuropeanOption
    {
        public double S { get; }
        public double K { get; }
        public double R { get; }
        public double Sigma { get; }
        public double T { get; }
        public bool IsCall { get; }

        public EuropeanOption(double s, double k, double r, double sigma, double t, bool isCall)
        {
            S = s;
            K = k;
            R = r;
            Sigma = sigma;
            T = t;
            IsCall = isCall;
        }

        // Price of the option
        public double Price()
        {
            return IsCall
                ? BlackScholes.CallPrice(S, K, R, Sigma, T)
                : BlackScholes.PutPrice(S, K, R, Sigma, T);
        }

        // Delta of the option
        public double Delta()
        {
            return IsCall
                ? BlackScholes.CallDelta(S, K, R, Sigma, T)
                : BlackScholes.PutDelta(S, K, R, Sigma, T);
        }
    }
}
