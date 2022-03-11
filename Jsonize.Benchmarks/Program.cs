// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Jsonize.Benchmarks.Benchmarks;

BenchmarkRunner.Run<JsonizeBenchmarks>();