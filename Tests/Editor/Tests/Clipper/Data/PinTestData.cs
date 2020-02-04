using iShape.Geometry;
using UnityEngine;

namespace Tests.Clipper.Data {

    public struct PinTestData {

        public static readonly Vector2[][][] data = {
            // 0
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(0, 0),
                    new Vector2(5, 10),
                    new Vector2(-5, 10)
                }
            },
            // 1
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(0, 0),
                    new Vector2(-10, 5),
                    new Vector2(-10, -5)
                }
            },
            // 2
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(0, 0),
                    new Vector2(5, -10),
                    new Vector2(-5, -10)
                }
            },
            // 3
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(0, 0),
                    new Vector2(10, -5),
                    new Vector2(10, 5)
                }
            },
            // 4
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(0, 0),
                    new Vector2(10, 10),
                    new Vector2(-10, 10)
                }
            },
            // 5
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(0, 0),
                    new Vector2(-10, 10),
                    new Vector2(-10, -10)
                }
            },
            // 6
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(0, 0),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                }
            },
            // 7
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(0, 0),
                    new Vector2(10, -10),
                    new Vector2(10, 10)
                }
            },
            // 8
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(10, 5),
                    new Vector2(10, 10),
                    new Vector2(-10, 10),
                    new Vector2(-10, 5)
                }
            },
            // 9
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(-5, 10),
                    new Vector2(-10, 10),
                    new Vector2(-10, -10),
                    new Vector2(-5, -10)
                }
            },
            // 10
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(-10, -5),
                    new Vector2(-10, -10),
                    new Vector2(10, -10),
                    new Vector2(10, -5)
                }
            },
            // 11
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(5, -10),
                    new Vector2(10, -10),
                    new Vector2(10, 10),
                    new Vector2(5, 10)
                }
            },
            // 12
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(10, 0),
                    new Vector2(10, 10),
                    new Vector2(0, 10)
                }
            },
            // 13
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(0, 10),
                    new Vector2(-10, 10),
                    new Vector2(-10, 0)
                }
            },
            // 14
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(-10, 0),
                    new Vector2(-10, -10),
                    new Vector2(0, -10)
                }
            },
            // 15
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(0, -10),
                    new Vector2(10, -10),
                    new Vector2(10, 0)
                }
            },
            // 16
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(0, 10),
                    new Vector2(-10, 10),
                    new Vector2(-10, -10),
                    new Vector2(10, -10),
                    new Vector2(10, 0)
                }
            },
            // 17
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(-10, 0),
                    new Vector2(-10, -10),
                    new Vector2(10, -10),
                    new Vector2(10, 10),
                    new Vector2(0, 10)
                }
            },
            // 18
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(0, -10),
                    new Vector2(10, -10),
                    new Vector2(10, 10),
                    new Vector2(-10, 10),
                    new Vector2(-10, 0)
                }
            },
            // 19
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(10, 0),
                    new Vector2(10, 10),
                    new Vector2(-10, 10),
                    new Vector2(-10, -10),
                    new Vector2(0, -10)
                }
            },
            // 20
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(-5, 10),
                    new Vector2(-5, 20),
                    new Vector2(-15, 20),
                    new Vector2(-15, 10)
                }
            },
            // 21
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(-5, 0),
                    new Vector2(-5, 10),
                    new Vector2(-15, 10),
                    new Vector2(-15, 0)
                }
            },
            // 22
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(5, 10),
                    new Vector2(5, 20),
                    new Vector2(15, 20),
                    new Vector2(15, 10)
                }
            },
            // 23
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(5, 0),
                    new Vector2(5, 10),
                    new Vector2(15, 10),
                    new Vector2(15, 0)
                }
            },
            // 24
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(10, 10),
                    new Vector2(10, 20),
                    new Vector2(-10, 20),
                    new Vector2(-10, 10)
                }
            },
            // 25
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(5, -10),
                    new Vector2(5, 10),
                    new Vector2(-5, 10),
                    new Vector2(-5, -10)
                }
            },
            // 26
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(-10, 0),
                    new Vector2(-10, 10),
                    new Vector2(-20, 10),
                    new Vector2(-20, 0)
                }
            },
            // 27
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, 0),
                    new Vector2(-10, 0)
                },
                new[] {
                    new Vector2(-15, -30),
                    new Vector2(-15, 30),
                    new Vector2(10 - IntGeom.DefGeom.Float(1), 30),
                    new Vector2(10 + IntGeom.DefGeom.Float(1), -30)
                }
            },
            // 28
            new[] {
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10)
                },
                new[] {
                    new Vector2(-15, -30),
                    new Vector2(-15, 30),
                    new Vector2(10 - IntGeom.DefGeom.Float(1), 30),
                    new Vector2(10 + IntGeom.DefGeom.Float(1), -30)
                }
            }
        };
    }
}