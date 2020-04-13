using iShape.Geometry;
using UnityEngine;

namespace Tests.Clipper.Data {
    
    internal struct SubtractTestData {
        
        internal static readonly Vector2[][][] data = {
            // 0
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(5.0f, -15.0f),
                    new Vector2(5.0f, 0.0f),
                    new Vector2(-5.0f, 0.0f),
                    new Vector2(-5.0f, -15.0f)
                }
            },
            // 1
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(-5.0f, -15.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(-5.0f, 15.0f)
                }
            },
            // 2
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(-5.0f, -15.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(-5.0f, 10.0f)
                }
            },
            // 3
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(-5.0f, -10.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(-5.0f, 10.0f)
                }
            },
            // 4
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(-5.0f, -10.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(-10.0f, 10.0f)
                }
            },
            // 5
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(0.0f, -10.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(-20.0f, 10.0f)
                }
            },
            // 6
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(15.0f, -15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(-15.0f, 0.0f)
                }
            },
            // 7
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(0.0f, -15.0f),
                    new Vector2(0.0f, 0.0f),
                    new Vector2(-15.0f, 0.0f)
                }
            },
            // 8
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(0.0f, 0.0f),
                    new Vector2(10.0f, 0.0f),
                    new Vector2(0.0f, 10.0f)
                }
            },
            // 9
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(-5.0f, -10.0f),
                    new Vector2(5.0f, -10.0f),
                    new Vector2(0.0f, 10.0f)
                }
            },
            // 10
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(-5.0f, 5.0f),
                    new Vector2(0.0f, -10.0f),
                    new Vector2(10.0f, 10.0f)
                }
            },
            // 11
            new[] {
                new[] {
                    new Vector2(0.0f, 10.0f),
                    new Vector2(20.0f, 10.0f),
                    new Vector2(20.0f, -10.0f),
                    new Vector2(0.0f, -10.0f)
                },
                new[] {
                    new Vector2(-20.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(20.0f, 10.0f)
                }
            },
            // 12
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(0.0f, 0.0f),
                    new Vector2(10.0f, -5.0f),
                    new Vector2(10.0f, 5.0f)
                }
            },
            // 13
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(5.0f, -10.0f),
                    new Vector2(-5.0f, -10.0f),
                    new Vector2(0.0f, -15.0f)
                }
            },
            // 14
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(-5.0f, 10.0f),
                    new Vector2(-10.0f, 5.0f),
                    new Vector2(-10.0f, -5.0f),
                    new Vector2(-5.0f, -10.0f),
                    new Vector2(5.0f, -10.0f),
                    new Vector2(10.0f, -5.0f),
                    new Vector2(10.0f, 5.0f),
                    new Vector2(5.0f, 10.0f)
                }
            },
            // 15
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(0.0f, 10.0f),
                    new Vector2(0.0f, 0.0f),
                    new Vector2(10.0f, 0.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(0.0f, 10.0f),
                    new Vector2(0.0f, -5.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(5.0f, 10.0f)
                }
            },
            // 16
            new[] {
                new[] {
                    new Vector2(-5.0f, 0.0f),
                    new Vector2(5.0f, 0.0f),
                    new Vector2(0.0f, -5.0f),
                    new Vector2(-5.0f, -5.0f)
                },
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(-10.0f, -5.0f),
                    new Vector2(-5.0f, -5.0f),
                    new Vector2(-5.0f, 5.0f),
                    new Vector2(5.0f, 5.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(10.0f, -5.0f),
                    new Vector2(10.0f, 10.0f)
                }
            },
            // 17
            new[] {
                new[] {
                    new Vector2(-7.5f, 10.0f),
                    new Vector2(12.5f, 10.0f),
                    new Vector2(12.5f, 5.0f),
                    new Vector2(-7.5f, 5.0f)
                },
                new[] {
                    new Vector2(0.0f, 0.0f),
                    new Vector2(0.0f, 20.0f),
                    new Vector2(-15.0f, 20.0f),
                    new Vector2(-15.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 30.0f),
                    new Vector2(-25.0f, 30.0f),
                    new Vector2(-25.0f, -20.0f),
                    new Vector2(20.0f, -20.0f),
                    new Vector2(20.0f, 20.0f),
                    new Vector2(15.0f, 20.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-20.0f, -15.0f),
                    new Vector2(-20.0f, 25.0f),
                    new Vector2(5.0f, 25.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(-10.0f, -5.0f),
                    new Vector2(-10.0f, 15.0f),
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(-5.0f, 0.0f)
                }
            },
            // 18
            new[] {
                new[] {
                    new Vector2(-7.5f, 2.5f),
                    new Vector2(12.5f, 2.5f),
                    new Vector2(12.5f, -2.5f),
                    new Vector2(-7.5f, -2.5f)
                },
                new[] {
                    new Vector2(0.0f, 0.0f),
                    new Vector2(0.0f, 20.0f),
                    new Vector2(-15.0f, 20.0f),
                    new Vector2(-15.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 30.0f),
                    new Vector2(-25.0f, 30.0f),
                    new Vector2(-25.0f, -20.0f),
                    new Vector2(20.0f, -20.0f),
                    new Vector2(20.0f, 20.0f),
                    new Vector2(15.0f, 20.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-20.0f, -15.0f),
                    new Vector2(-20.0f, 25.0f),
                    new Vector2(5.0f, 25.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(-10.0f, -5.0f),
                    new Vector2(-10.0f, 15.0f),
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(-5.0f, 0.0f)
                }
            },
            // 19
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(10.0f, 0.0f),
                    new Vector2(0.0f, 5.0f),
                    new Vector2(0.0f, -5.0f)
                }
            },
            // 20
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(10.0f, 10.0f),
                    new Vector2(0.0f, 5.0f),
                    new Vector2(5.0f, 0.0f)
                }
            },
            // 21
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(0.0f, 5.0f),
                    new Vector2(10.0f, 0.0f),
                    new Vector2(10.0f, -5.0f),
                    new Vector2(15.0f, 5.0f)
                }
            },
            // 22
            new[] {
                new[] {
                    new Vector2(-5.0f, 0.0f),
                    new Vector2(5.0f, 0.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(-5.0f, -5.0f)
                },
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(-10.0f, -10.0f),
                    new Vector2(0.0f, -10.0f),
                    new Vector2(0.0f, 5.0f),
                    new Vector2(5.0f, 5.0f),
                    new Vector2(5.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 10.0f)
                }
            },
            // 23
            new[] {
                new[] {
                    new Vector2(5.0f, 15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-15.0f, -15.0f),
                    new Vector2(-15.0f, -5.0f),
                    new Vector2(5.0f, -5.0f)
                },
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(-10.0f, 5.0f),
                    new Vector2(5.0f, 5.0f),
                    new Vector2(5.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 10.0f)
                }
            },
            // 24
            new[] {
                new[] {
                    new Vector2(5.0f, 15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-15.0f, -15.0f),
                    new Vector2(-15.0f, -5.0f),
                    new Vector2(5.0f, -5.0f)
                },
                new[] {
                    new Vector2(-5.0f, 5.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(15.0f, -5.0f),
                    new Vector2(15.0f, 5.0f)
                }
            },
            // 25
            new[] {
                new[] {
                    new Vector2(5.0f, 15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-15.0f, -15.0f),
                    new Vector2(-15.0f, -5.0f),
                    new Vector2(5.0f, -5.0f)
                },
                new[] {
                    new Vector2(-5.0f, 5.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(25.0f, 0.0f),
                    new Vector2(15.0f, 5.0f)
                }
            },
            // 26
            new[] {
                new[] {
                    new Vector2(5.0f, 15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-15.0f, -15.0f),
                    new Vector2(-15.0f, -5.0f),
                    new Vector2(5.0f, -5.0f)
                },
                new[] {
                    new Vector2(5.0f, 15.0f),
                    new Vector2(5.0f, -10.0f),
                    new Vector2(15.0f, -10.0f),
                    new Vector2(15.0f, 5.0f)
                }
            },
            // 27
            new[] {
                new[] {
                    new Vector2(5.0f, 15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-15.0f, -15.0f),
                    new Vector2(-15.0f, -5.0f),
                    new Vector2(5.0f, -5.0f)
                },
                new[] {
                    new Vector2(-10.0f, 0.0f),
                    new Vector2(-10.0f, -10.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(10.0f, 0.0f)
                }
            },
            // 28
            new[] {
                new[] {
                    new Vector2(-10.0f, -5.0f),
                    new Vector2(-10.0f, 15.0f),
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(-5.0f, -5.0f)
                },
                new[] {
                    new Vector2(0.0f, 5.0f),
                    new Vector2(-20.0f, 5.0f),
                    new Vector2(-20.0f, -15.0f),
                    new Vector2(10.0f, -15.0f),
                    new Vector2(10.0f, 20.0f),
                    new Vector2(-20.0f, 20.0f),
                    new Vector2(-20.0f, 10.0f),
                    new Vector2(5.0f, 10.0f),
                    new Vector2(5.0f, -10.0f),
                    new Vector2(-15.0f, -10.0f),
                    new Vector2(-15.0f, 0.0f),
                    new Vector2(0.0f, 0.0f)
                }
            },
            // 29
            new[] {
                new[] {
                    new Vector2(-18.5f, -3.5f),
                    new Vector2(-15.0f, 10.0f),
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(-10.0f, 7.0f)
                },
                new[] {
                    new Vector2(0.0f, 5.0f),
                    new Vector2(-20.0f, 5.0f),
                    new Vector2(-20.0f, -15.0f),
                    new Vector2(10.0f, -15.0f),
                    new Vector2(10.0f, 20.0f),
                    new Vector2(-20.0f, 20.0f),
                    new Vector2(-20.0f, 10.0f),
                    new Vector2(5.0f, 10.0f),
                    new Vector2(5.0f, -10.0f),
                    new Vector2(-15.0f, -10.0f),
                    new Vector2(-15.0f, 0.0f),
                    new Vector2(0.0f, 0.0f)
                }
            },
            // 30
            new[] {
                new[] {
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(0.0f, 15.0f),
                    new Vector2(0.0f, 3.0f),
                    new Vector2(-5.0f, 3.0f)
                },
                new[] {
                    new Vector2(5.0f, 5.0f),
                    new Vector2(-15.0f, 5.0f),
                    new Vector2(-15.0f, -15.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(15.0f, 20.0f),
                    new Vector2(-15.0f, 20.0f),
                    new Vector2(-15.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f),
                    new Vector2(-10.0f, 0.0f),
                    new Vector2(5.0f, 0.0f)
                }
            },
            // 31
            new[] {
                new[] {
                    new Vector2(-10.0f, 15.0f),
                    new Vector2(10.0f, 15.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-11.0f, -5.0f)
                },
                new[] {
                    new Vector2(0.0f, 0.0f),
                    new Vector2(0.0f, 20.0f),
                    new Vector2(-15.0f, 20.0f),
                    new Vector2(-15.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 30.0f),
                    new Vector2(-25.0f, 30.0f),
                    new Vector2(-25.0f, -20.0f),
                    new Vector2(20.0f, -20.0f),
                    new Vector2(20.0f, 20.0f),
                    new Vector2(15.0f, 20.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-20.0f, -15.0f),
                    new Vector2(-20.0f, 25.0f),
                    new Vector2(5.0f, 25.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(-10.0f, -5.0f),
                    new Vector2(-10.0f, 15.0f),
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(-5.0f, 0.0f)
                }
            },
            // 32
            new[] {
                new[] {
                    new Vector2(-10.0f, 15.0f),
                    new Vector2(10.0f, 15.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-14.0f, -4.0f)
                },
                new[] {
                    new Vector2(0.0f, 0.0f),
                    new Vector2(0.0f, 20.0f),
                    new Vector2(-15.0f, 20.0f),
                    new Vector2(-15.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 30.0f),
                    new Vector2(-25.0f, 30.0f),
                    new Vector2(-25.0f, -20.0f),
                    new Vector2(20.0f, -20.0f),
                    new Vector2(20.0f, 20.0f),
                    new Vector2(15.0f, 20.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-20.0f, -15.0f),
                    new Vector2(-20.0f, 25.0f),
                    new Vector2(5.0f, 25.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(-10.0f, -5.0f),
                    new Vector2(-10.0f, 15.0f),
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(-5.0f, 0.0f)
                }
            },
            // 33
            new[] {
                new[] {
                    new Vector2(-23.0f, 20.0f),
                    new Vector2(7.0f, 20.0f),
                    new Vector2(7.0f, -16.0f),
                    new Vector2(-20.0f, -15.0f)
                },
                new[] {
                    new Vector2(0.0f, 0.0f),
                    new Vector2(0.0f, 20.0f),
                    new Vector2(-15.0f, 20.0f),
                    new Vector2(-15.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 30.0f),
                    new Vector2(-25.0f, 30.0f),
                    new Vector2(-25.0f, -20.0f),
                    new Vector2(20.0f, -20.0f),
                    new Vector2(20.0f, 20.0f),
                    new Vector2(15.0f, 20.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-20.0f, -15.0f),
                    new Vector2(-20.0f, 25.0f),
                    new Vector2(5.0f, 25.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(-10.0f, -5.0f),
                    new Vector2(-10.0f, 15.0f),
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(-5.0f, 0.0f)
                }
            },
            // 34
            new[] {
                new[] {
                    new Vector2(-23.0f, 20.0f),
                    new Vector2(7.0f, 20.0f),
                    new Vector2(4.0f, -16.0f),
                    new Vector2(-20.0f, -15.0f)
                },
                new[] {
                    new Vector2(0.0f, 0.0f),
                    new Vector2(0.0f, 20.0f),
                    new Vector2(-15.0f, 20.0f),
                    new Vector2(-15.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 30.0f),
                    new Vector2(-25.0f, 30.0f),
                    new Vector2(-25.0f, -20.0f),
                    new Vector2(20.0f, -20.0f),
                    new Vector2(20.0f, 20.0f),
                    new Vector2(15.0f, 20.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-20.0f, -15.0f),
                    new Vector2(-20.0f, 25.0f),
                    new Vector2(5.0f, 25.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(-10.0f, -5.0f),
                    new Vector2(-10.0f, 15.0f),
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(-5.0f, 0.0f)
                }
            },
            // 35
            new[] {
                new[] {
                    new Vector2(-20.0f, -15.0f),
                    new Vector2(12.0f, -5.0f),
                    new Vector2(15.0f, -9.0f),
                    new Vector2(-15.0f, -15.0f)
                },
                new[] {
                    new Vector2(0.0f, 0.0f),
                    new Vector2(0.0f, 20.0f),
                    new Vector2(-15.0f, 20.0f),
                    new Vector2(-15.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 30.0f),
                    new Vector2(-25.0f, 30.0f),
                    new Vector2(-25.0f, -20.0f),
                    new Vector2(20.0f, -20.0f),
                    new Vector2(20.0f, 20.0f),
                    new Vector2(15.0f, 20.0f),
                    new Vector2(15.0f, -14.0f),
                    new Vector2(-20.0f, -14.0f),
                    new Vector2(-20.0f, 25.0f),
                    new Vector2(5.0f, 25.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(-10.0f, -5.0f),
                    new Vector2(-10.0f, 15.0f),
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(-5.0f, 0.0f)
                }
            },
            // 36
            new[] {
                new[] {
                    new Vector2(5.0f, 10.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(-10.0f, 10.0f)
                },
                new[] {
                    new Vector2(0.0f, 15.0f),
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(-5.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 0.0f),
                    new Vector2(0.0f, 0.0f)
                }
            },
            // 37
            new[] {
                new[] {
                    new Vector2(5.0f, 10.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(-2.0f, 2.0f),
                    new Vector2(2.0f, 6.0f),
                    new Vector2(-2.0f, 10.0f)
                },
                new[] {
                    new Vector2(0.0f, 15.0f),
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(-5.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 0.0f),
                    new Vector2(0.0f, 0.0f)
                }
            },
            // 38
            new[] {
                new[] {
                    new Vector2(-5.0f, 0.0f),
                    new Vector2(10.0f, 0.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(-10.0f, -10.0f),
                    new Vector2(15.0f, -10.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-15.0f, -15.0f),
                    new Vector2(-15.0f, 15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(15.0f, -5.0f),
                    new Vector2(-5.0f, -5.0f)
                },
                new[] {
                    new Vector2(0.0f, 0.0f),
                    new Vector2(0.0f, 20.0f),
                    new Vector2(-15.0f, 20.0f),
                    new Vector2(-15.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 30.0f),
                    new Vector2(-25.0f, 30.0f),
                    new Vector2(-25.0f, -20.0f),
                    new Vector2(20.0f, -20.0f),
                    new Vector2(20.0f, 20.0f),
                    new Vector2(15.0f, 20.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-20.0f, -15.0f),
                    new Vector2(-20.0f, 25.0f),
                    new Vector2(5.0f, 25.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(-10.0f, -5.0f),
                    new Vector2(-10.0f, 15.0f),
                    new Vector2(-5.0f, 15.0f),
                    new Vector2(-5.0f, 0.0f)
                }
            },
            // 39
            new[] {
                new[] {
                    new Vector2(5.0f, 15.0f),
                    new Vector2(15.0f, 15.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(-15.0f, -15.0f),
                    new Vector2(-15.0f, -5.0f),
                    new Vector2(5.0f, -5.0f)
                },
                new[] {
                    new Vector2(-5.0f, 5.0f),
                    new Vector2(-20.0f, -10.0f),
                    new Vector2(15.0f, -10.0f),
                    new Vector2(18.0f, 16.5f)
                }
            },
            // 40
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(-10.0f, -10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(10.0f, 10.0f)
                }
            },
            // 41
            new[] {
                new[] {
                    new Vector2(-10.0f, 5.0f),
                    new Vector2(10.0f, 5.0f),
                    new Vector2(10.0f, -5.0f),
                    new Vector2(-10.0f, -5.0f)
                },
                new[] {
                    new Vector2(-5.0f, 10.0f),
                    new Vector2(-5.0f, -5.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(5.0f, -10.0f),
                    new Vector2(15.0f, -10.0f),
                    new Vector2(15.0f, 10.0f)
                }
            },
            // 42
            new[] {
                new[] {
                    new Vector2(-10.0f, 5.0f),
                    new Vector2(10.0f, 5.0f),
                    new Vector2(10.0f, -5.0f),
                    new Vector2(-10.0f, -5.0f)
                },
                new[] {
                    new Vector2(-5.0f, 10.0f),
                    new Vector2(-5.0f, -5.0f),
                    new Vector2(10.0f, -5.0f),
                    new Vector2(10.0f, 0.0f),
                    new Vector2(15.0f, 0.0f),
                    new Vector2(15.0f, 10.0f)
                }
            },
            // 43
            new[] {
                new[] {
                    new Vector2(-10.0f, 10.0f),
                    new Vector2(10.0f, 10.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(0.0f, 5.0f),
                    new Vector2(10.0f, 0.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(0.0f, -10.0f),
                    new Vector2(0.0f, -15.0f),
                    new Vector2(15.0f, -15.0f),
                    new Vector2(15.0f, 5.0f)
                }
            },
            // 44
            new[] {
                new[] {
                    new Vector2(-10.0f, 5.0f),
                    new Vector2(10.0f, 5.0f),
                    new Vector2(10.0f, -10.0f),
                    new Vector2(-10.0f, -10.0f)
                },
                new[] {
                    new Vector2(-5.0f, 5.0f),
                    new Vector2(5.0f, 5.0f),
                    new Vector2(5.0f, -5.0f),
                    new Vector2(15.0f, -5.0f),
                    new Vector2(15.0f, 10.0f),
                    new Vector2(-15.0f, 10.0f),
                    new Vector2(-15.0f, -5.0f),
                    new Vector2(-5.0f, -5.0f)
                }
            },
            // 45
            new[] {
                new[] {
                    new Vector2(1f, -0.0001f),
                    new Vector2(0f, 0.9999f),
                    new Vector2(-1f, -0.0001f),
                    new Vector2(-3f,  -0.0001f),
                    new Vector2(-3f,  2.9999f),
                    new Vector2(3f,  2.9999f),
                    new Vector2(3f,  -0.0001f)
                },
                new[] {
                    new Vector2(1f, 0f),
                    new Vector2(0f, 1f),
                    new Vector2(-1f, 0f),
                    new Vector2(0f, -1f)
                }
            },
            // 46
            new[] {
                new[] {
                    new Vector2(10, -10),
                    new Vector2(-10, -10),
                    new Vector2(-10, 5),
                    new Vector2(10,  5)
                },
                new[] {
                    new Vector2(-20, 15),
                    new Vector2(-20, 5),
                    new Vector2(20, 5),
                    new Vector2(20, 15)
                }
            },
            // 47
            new[] {
                new[] {
                    new IntVector(20173, -7262),
                    new IntVector(19201, -7433),
                    new IntVector(17047, -10000),
                    new IntVector(-30000, -10000),
                    new IntVector(-30000, 40000),
                    new IntVector(30000, 40000),
                    new IntVector(30000, -5388),
                    new IntVector(23624, -6512),
                    new IntVector(23617, -6520),
                    new IntVector(23317, -6573),
                    new IntVector(23299, -6595),
                    new IntVector(23071, -6635),
                    new IntVector(23054, -6656),
                    new IntVector(22826, -6696),
                    new IntVector(22798, -6729),
                    new IntVector(22641, -6757),
                    new IntVector(22602, -6804),
                    new IntVector(22518, -6819),
                    new IntVector(20430, -7187),
                    new IntVector(20412, -7209),
                    new IntVector(20184, -7249)
                }.toFloat(1),
                new[] {
                    new IntVector(34140, -16093),
                    new IntVector(31800, -9665),
                    new IntVector(25877, -6245),
                    new IntVector(19140, -7433),
                    new IntVector(14743, -12673),
                    new IntVector(14743, -19514),
                    new IntVector(19140, -24754),
                    new IntVector(25877, -25941),
                    new IntVector(31800, -22521)
                }.toFloat(1)
            }
        };
    }
}