using System;
using System.Collections.Generic;
using System.Text;

namespace AStar_vs_Best_First_Analysis {
    public class UnsolvableException : Exception {
        public UnsolvableException(string msg) : base(msg) { }
    }

    public struct SearchResult {
        public bool PathFound;
        public int VisitedCells;
    }

    public class AnalysisResult {
        public string Algo;
        public int Size;
        public int Percent;
        public double AvgVisited;
        public int Unsolvable;
    }

    public class Validator {
        public void Check(SearchResult res) {
            if (!res.PathFound) throw new UnsolvableException("Path blocked.");
        }
    }

    public class Analyzer {
        private LabyrinthGenerator _gen;
        private Validator _val;

        public Analyzer(LabyrinthGenerator g, Validator v) { _gen = g; _val = v; }

        public List<AnalysisResult> RunAnalysis(InputConfig config) {
            var results = new List<AnalysisResult>();
            var algos = new PathfinderBase[] { new AStar(), new BestFirstSearch() };

            foreach (var size in config.Sizes) {
                foreach (var p in config.WallPercentages) {
                    foreach (var algo in algos) {
                        int totalVisited = 0, unsolvable = 0, solvedCount = 0;
                        string name = algo.GetType().Name;

                        for (int i = 0; i < config.Iterations; i++) {
                            var grid = _gen.Generate(size, p, out bool forced);
                            var res = algo.Search(grid);
                            try {
                                _val.Check(res);
                                totalVisited += res.VisitedCells;
                                solvedCount++;
                            } catch { unsolvable++; }
                        }

                        results.Add(new AnalysisResult {
                            Algo = name, Size = size, Percent = p,
                            AvgVisited = solvedCount > 0 ? (double)totalVisited / solvedCount : 0,
                            Unsolvable = unsolvable
                        });
                    }
                }
            }
            return results;
        }
    }

    public class Comparator {
        public string GenerateCsv(List<AnalysisResult> results) {
            var sb = new StringBuilder();
            foreach (var r in results)
                sb.AppendLine($"{r.Algo}; {r.Size}; {r.Percent}; {r.AvgVisited:F2}; {r.Unsolvable}");
            return sb.ToString();
        }
    }

    public class InputConfig {
        public List<int> Sizes;
        public List<int> WallPercentages;
        public int Iterations;
    }
}