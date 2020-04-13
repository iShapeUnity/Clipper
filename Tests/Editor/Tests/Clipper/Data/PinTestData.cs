using iShape.Geometry;
using UnityEngine;

namespace Tests.Clipper.Data {

    internal struct PinTestData {

        internal static readonly IntVector[][][] data = {
            // 0
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(0, 0),
                    new Vector2(5, 10),
                    new Vector2(-5, 10)
                }.toInt()
            },
            // 1
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(0, 0),
                    new Vector2(-10, 5),
                    new Vector2(-10, -5)
                }.toInt()
            },
            // 2
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(0, 0),
                    new Vector2(5, -10),
                    new Vector2(-5, -10)
                }.toInt()
            },
            // 3
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(0, 0),
                    new Vector2(10, -5),
                    new Vector2(10, 5)
                }.toInt()
            },
            // 4
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(0, 0),
                    new Vector2(10, 10),
                    new Vector2(-10, 10)
                }.toInt()
            },
            // 5
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(0, 0),
                    new Vector2(-10, 10),
                    new Vector2(-10, -10)
                }.toInt()
            },
            // 6
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(0, 0),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt()
            },
            // 7
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(0, 0),
                    new Vector2(10, -10),
                    new Vector2(10, 10)
                }.toInt()
            },
            // 8
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(10, 5),
                    new Vector2(10, 10),
                    new Vector2(-10, 10),
                    new Vector2(-10, 5)
                }.toInt()
            },
            // 9
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(-5, 10),
                    new Vector2(-10, 10),
                    new Vector2(-10, -10),
                    new Vector2(-5, -10)
                }.toInt()
            },
            // 10
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(-10, -5),
                    new Vector2(-10, -10),
                    new Vector2(10, -10),
                    new Vector2(10, -5)
                }.toInt()
            },
            // 11
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(5, -10),
                    new Vector2(10, -10),
                    new Vector2(10, 10),
                    new Vector2(5, 10)
                }.toInt()
            },
            // 12
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(10, 0),
                    new Vector2(10, 10),
                    new Vector2(0, 10)
                }.toInt()
            },
            // 13
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(0, 10),
                    new Vector2(-10, 10),
                    new Vector2(-10, 0)
                }.toInt()
            },
            // 14
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(-10, 0),
                    new Vector2(-10, -10),
                    new Vector2(0, -10)
                }.toInt()
            },
            // 15
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(0, -10),
                    new Vector2(10, -10),
                    new Vector2(10, 0)
                }.toInt()
            },
            // 16
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(0, 10),
                    new Vector2(-10, 10),
                    new Vector2(-10, -10),
                    new Vector2(10, -10),
                    new Vector2(10, 0)
                }.toInt()
            },
            // 17
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(-10, 0),
                    new Vector2(-10, -10),
                    new Vector2(10, -10),
                    new Vector2(10, 10),
                    new Vector2(0, 10)
                }.toInt()
            },
            // 18
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(0, -10),
                    new Vector2(10, -10),
                    new Vector2(10, 10),
                    new Vector2(-10, 10),
                    new Vector2(-10, 0)
                }.toInt()
            },
            // 19
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(10, 0),
                    new Vector2(10, 10),
                    new Vector2(-10, 10),
                    new Vector2(-10, -10),
                    new Vector2(0, -10)
                }.toInt()
            },
            // 20
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(-5, 10),
                    new Vector2(-5, 20),
                    new Vector2(-15, 20),
                    new Vector2(-15, 10)
                }.toInt()
            },
            // 21
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(-5, 0),
                    new Vector2(-5, 10),
                    new Vector2(-15, 10),
                    new Vector2(-15, 0)
                }.toInt()
            },
            // 22
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(5, 10),
                    new Vector2(5, 20),
                    new Vector2(15, 20),
                    new Vector2(15, 10)
                }.toInt()
            },
            // 23
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(5, 0),
                    new Vector2(5, 10),
                    new Vector2(15, 10),
                    new Vector2(15, 0)
                }.toInt()
            },
            // 24
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(10, 10),
                    new Vector2(10, 20),
                    new Vector2(-10, 20),
                    new Vector2(-10, 10)
                }.toInt()
            },
            // 25
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(5, -10),
                    new Vector2(5, 10),
                    new Vector2(-5, 10),
                    new Vector2(-5, -10)
                }.toInt()
            },
            // 26
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(-10, 0),
                    new Vector2(-10, 10),
                    new Vector2(-20, 10),
                    new Vector2(-20, 0)
                }.toInt()
            },
            // 27
            new[] {
                new[] {
                    new IntVector(-100_000, 100_000),
                    new IntVector(100_000, 100_000),
                    new IntVector(100_000, 0),
                    new IntVector(-100_000, 0)
                },
                new[] {
                    new IntVector(-1500_000, -300_000),
                    new IntVector(-1500_000, 300_000),
                    new IntVector(99_999, 300_000),
                    new IntVector(100_001, -300_000)
                }
            },
            // 28
            new[] {
                new[] {
                    new IntVector(-100_000, 100_000),
                    new IntVector(100_000, 100_000),
                    new IntVector(100_000, -100_000),
                    new IntVector(-100_000, -100_000)
                },
                new[] {
                    new IntVector(-150_000, -300_000),
                    new IntVector(-150_000, 300_000),
                    new IntVector(99_999, 300_000),
                    new IntVector(100_001, -300_000)
                }
            },
            // 29
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt(),
                new[] {
                    new Vector2(20, -5),
                    new Vector2(20, 5),
                    new Vector2(10, 5),
                    new Vector2(10, 0),
                    new Vector2(10, -5)
                }.toInt()
            },
            // 30
            new[] {
                new[] {
                    new Vector2(20, -5),
                    new Vector2(20, 5),
                    new Vector2(10, 5),
                    new Vector2(10, 0),
                    new Vector2(10, -5)
                }.toInt(),
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt()
            },
            // 31
            new[] {
                new[] {
                    new Vector2(20, -5),
                    new Vector2(20, 5),
                    new Vector2(10, 5),
                    new Vector2(10, 0),
                    new Vector2(10, -5)
                }.toInt(),
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, 0),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }.toInt()
            },
            // 32
            new[] {
                new[] {
                    new IntVector(4_410, 2_200),
                    new IntVector(4_638, 2_160),
                    new IntVector(12_909, 0),
                    new IntVector(-10_000, 0),
                    new IntVector(-10_000, 10_000),
                    new IntVector(4_410, 10_000)
                },
                new[] {
                    new IntVector(6_970, 15_000),
                    new IntVector(0, 15_000),
                    new IntVector(233, 2_937),
                    new IntVector(6_970, 1_749)
                }
            },
            // 33
            new[] {
                new[] {
                    new IntVector(100_000, 50_000),
                    new IntVector(100_000, 200_000),
                    new IntVector(-100_000, 200_000),
                    new IntVector(-100_000, -200_000),
                    new IntVector(100_000, -200_000),
                    new IntVector(100_000, -50_000),
                    new IntVector(150_000, -50_000),
                    new IntVector(150_000, -250_000),
                    new IntVector(-150_000, -250_000),
                    new IntVector(-150_000, 250_000),
                    new IntVector(150_000, 250_000),
                    new IntVector(150_000, 50_000)
                },
                new[] {
                    new IntVector(-200_000, 50_000),
                    new IntVector(-200_000, -50_000),
                    new IntVector(200_000, -50_000),
                    new IntVector(200_000, 50_000)
                }
            },
            // 34
            new[] {
                new[] {
                    new IntVector(0, 100_000),
                    new IntVector(200_000, 100_000),
                    new IntVector(200_000, -100_000),
                    new IntVector(0, -100_000)
                },
                new[] {
                    new IntVector(-200_000, -100_000),
                    new IntVector(100_000, -100_000),
                    new IntVector(200_000, 100_000)
                }
            },
            // 35
            new[] {
                new[] {
                    new IntVector(-18, -7),
                    new IntVector(-18, -6_848),
                    new IntVector(-10_000, -6848),
                    new IntVector(-10_000, 10_000),
                    new IntVector(5_000, 10_000),
                    new IntVector(4_450, 5_515),
                    new IntVector(-18, 134)
                },
                new[] {
                    new IntVector(19_379, -3_004),
                    new IntVector(17_040, 3_424),
                    new IntVector(11_116, 6_844),
                    new IntVector(4_379, 5_656),

                    new IntVector(-18, 416),
                    new IntVector(-18, -6_424),
                    new IntVector(4_379, -11_664),
                    new IntVector(11_116, -12_852),
                    new IntVector(17_040, -9_432)
                }
            }
        };
    }
}