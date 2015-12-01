﻿// Copyright (c) Petabridge <https://petabridge.com/>. All rights reserved.
// Licensed under the Apache 2.0 license. See LICENSE file in the project root for full license information.

using System;

namespace NBench.Reporting.Targets
{

    //TODO: https://github.com/petabridge/NBench/issues/4
    /// <summary>
    /// Output writer to the console for NBench
    /// </summary>
    public class ConsoleBenchmarkOutput : IBenchmarkOutput
    {
        public void WriteLine(string message)
        {
            Console.WriteLine("--------------- STARTING {0} ---------------", message);
        }

        public void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("WARNING: " + message);
            Console.ResetColor();
        }

        public void Error(Exception ex, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: " + message);
            Console.WriteLine(ex);
            Console.ResetColor();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: " + message);
            Console.ResetColor();
        }

        public void WriteRun(BenchmarkRunReport report, bool isWarmup = false)
        {
            if (isWarmup)
                Console.WriteLine("--------------- BEGIN WARMUP ---------------");
            else
            {
                Console.WriteLine("--------------- BEGIN RUN ---------------");
            }
           
            Console.WriteLine("Elapsed: {0}", report.Elapsed);
            foreach (var metric in report.Metrics.Values)
            {
                Console.WriteLine("{0} - {1}: {2:n} ,{1}: /s {3:n} , ns / {1}: {4:n}", metric.Name, metric.Unit, metric.MetricValue, metric.MetricValuePerSecond, metric.NanosPerMetricValue);
            }
                
           
            
            if (isWarmup)
                Console.WriteLine("--------------- END WARMUP ---------------");
            else
            {
                Console.WriteLine("--------------- END RUN ---------------");
            }
            Console.WriteLine();
        }

        public void WriteBenchmark(BenchmarkFinalResults results)
        {
            Console.WriteLine("--------------- RESULTS: {0} ---------------", results.BenchmarkName);

            Console.WriteLine("--------------- DATA ---------------");
            foreach (var metric in results.Data.StatsByMetric.Values)
            {
                Console.WriteLine("{0}: Max: {2:n} {1}, Average: {3:n} {1}, Min: {4:n} {1}, StdDev: {5:n} {1}", metric.Name,
                    metric.Unit, metric.Stats.Max, metric.Stats.Average, metric.Stats.Min, metric.Stats.StandardDeviation);

                Console.WriteLine("{0}: Max / s: {2:n} {1}, Average / s: {3:n} {1}, Min / s: {4:n} {1}, StdDev / s: {5:n} {1}", metric.Name,
                    metric.Unit, metric.PerSecondStats.Max, metric.PerSecondStats.Average, metric.PerSecondStats.Min, metric.PerSecondStats.StandardDeviation);
                Console.WriteLine();
            }

            if (results.AssertionResults.Count > 0)
            {
                Console.WriteLine("--------------- ASSERTIONS ---------------");
                foreach (var assertion in results.AssertionResults)
                {
                    Console.ForegroundColor = assertion.Passed ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;
                    Console.WriteLine(assertion.Message);
                }
            }
                

            Console.WriteLine();
        }
    }
}
