using iShape.Geometry;
using iShape.Geometry.Container;
using Unity.Collections;
using UnityEngine;

namespace Tests.Clipper.Data {

    internal struct BiteTestData {

        internal struct PackData {
            internal PlainShape shape;
            internal NativeArray<IntVector> path;

            internal PackData(PlainShape shape, NativeArray<IntVector> path) {
                this.shape = shape;
                this.path = path;
            }
            
            internal void Dispose() {
                this.shape.Dispose();
                this.path.Dispose();
            }
        }

        internal struct TestData {

            private readonly Vector2[] hull;
            private readonly Vector2[][] holes;
            private readonly Vector2[] path;

            internal TestData(Vector2[] hull, Vector2[][] holes, Vector2[] path) {
                this.hull = hull;
                this.holes = holes;
                this.path = path;
            }

            internal PackData Allocate(Allocator allocator) {
                var iHull = IntGeom.DefGeom.Int(hull);
                var iHoles = IntGeom.DefGeom.Int(holes);
                var iShape = new IntShape(iHull, iHoles);

                var iPath = IntGeom.DefGeom.Int(path);

                var packData = new PackData(
                    new PlainShape(iShape, allocator),
                    new NativeArray<IntVector>(iPath, allocator)
                    );
                
                return packData;
            }

        }


        internal static readonly TestData[] data = {
            // 0
            new TestData(new[] {
                    new Vector2(-15, -15),
                    new Vector2(-15, 15),
                    new Vector2(15, 15),
                    new Vector2(15, -15)
                }, new[] {
                    new[] {
                        new Vector2(-10, 10),
                        new Vector2(-10, -10),
                        new Vector2(10, -10),
                        new Vector2(10, 10)
                    }
                }, new[] {
                    new Vector2(-5, 5),
                    new Vector2(-5, -5),
                    new Vector2(5, -5),
                    new Vector2(5, 5)
                }
            ),
            // 1
            new TestData(new[] {
                    new Vector2(-10, -10),
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10)
                },
                new Vector2[0][],
                new[] {
                    new Vector2(-5, 5),
                    new Vector2(-5, -5),
                    new Vector2(5, -5),
                    new Vector2(5, 5)
                }
            ),
            // 2
            new TestData(new[] {
                    new Vector2(-15, 15),
                    new Vector2(15, 15),
                    new Vector2(15, -15),
                    new Vector2(-15, -15)
                }, new[] {
                    new[] {
                        new Vector2(-10, 10),
                        new Vector2(-10, -5),
                        new Vector2(5, -5),
                        new Vector2(5, 10)
                    }
                }, new[] {
                    new Vector2(-5, 5),
                    new Vector2(-5, -10),
                    new Vector2(10, -10),
                    new Vector2(10, 5)
                }
            ),
            // 3
            new TestData(new[] {
                    new Vector2(-15, 10),
                    new Vector2(15, 10),
                    new Vector2(15, -10),
                    new Vector2(-15, -10)
                }, new[] {
                    new[] {
                        new Vector2(-10, 5),
                        new Vector2(-10, 0),
                        new Vector2(0, 0),
                        new Vector2(0, 5)
                    },
                    new[] {
                        new Vector2(0, 0),
                        new Vector2(0, -5),
                        new Vector2(10, -5),
                        new Vector2(10, 0)
                    }
                }, new[] {
                    new Vector2(-5, 5),
                    new Vector2(-5, -5),
                    new Vector2(5, -5),
                    new Vector2(5, 5)
                }
            ),
            // 4
            new TestData(new[] {
                    new Vector2(-30, 35),
                    new Vector2(30, 35),
                    new Vector2(30, -35),
                    new Vector2(-30, -35)
                }, new[] {
                    new[] {
                        new Vector2(-5, 15),
                        new Vector2(-5, 10),
                        new Vector2(5, 10),
                        new Vector2(5, 15)
                    },
                    new[] {
                        new Vector2(-15, 5),
                        new Vector2(-15, 0),
                        new Vector2(15, 0),
                        new Vector2(15, 5)
                    },
                    new[] {
                        new Vector2(-5, -5),
                        new Vector2(-5, -10),
                        new Vector2(5, -10),
                        new Vector2(5, -5)
                    },
                    new[] {
                        new Vector2(-15, -15),
                        new Vector2(-15, -20),
                        new Vector2(15, -20),
                        new Vector2(15, -15)
                    },
                    new[] {
                        new Vector2(-5, -25),
                        new Vector2(-5, -30),
                        new Vector2(5, -30),
                        new Vector2(5, -25)
                    }
                }, new[] {
                    new Vector2(-20, 25),
                    new Vector2(-20, -25),
                    new Vector2(-10, -25),
                    new Vector2(-10, 20),
                    new Vector2(10, 20),
                    new Vector2(10, -25),
                    new Vector2(20, -25),
                    new Vector2(20, 25)
                }
            ),
            // 5
            new TestData(new[] {
                    new Vector2(-30, 35),
                    new Vector2(30, 35),
                    new Vector2(30, -40),
                    new Vector2(-30, -40)
                }, new[] {
                    new[] {
                        new Vector2(-5, 15),
                        new Vector2(-5, 10),
                        new Vector2(5, 10),
                        new Vector2(5, 15)
                    },
                    new[] {
                        new Vector2(-15, 5),
                        new Vector2(-15, 0),
                        new Vector2(15, 0),
                        new Vector2(15, 5)
                    },
                    new[] {
                        new Vector2(-5, -5),
                        new Vector2(-5, -10),
                        new Vector2(5, -10),
                        new Vector2(5, -5)
                    },
                    new[] {
                        new Vector2(-15, -15),
                        new Vector2(-15, -20),
                        new Vector2(15, -20),
                        new Vector2(15, -15)
                    },
                    new[] {
                        new Vector2(-5, -25),
                        new Vector2(-5, -30),
                        new Vector2(5, -30),
                        new Vector2(5, -25)
                    }
                }, new[] {
                    new Vector2(-20, 25),
                    new Vector2(-20, -25),
                    new Vector2(-10, -25),
                    new Vector2(-10, 20),
                    new Vector2(0, 20),
                    new Vector2(0, -35),
                    new Vector2(20, -35),
                    new Vector2(20, 25)
                }
            ),
            // 6
            new TestData(new[] {
                    new Vector2(-10, -10),
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10)
                },
                new Vector2[0][],
                new[] {
                    new Vector2(-15, 15),
                    new Vector2(-15, -15),
                    new Vector2(15, -15),
                    new Vector2(15, 15)
                }
            ),
            // 7
            new TestData(new[] {
                    new Vector2(-10, -10),
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10)
                },
                new Vector2[0][],
                new[] {
                    new Vector2(-10, 10),
                    new Vector2(-10, -10),
                    new Vector2(10, -10),
                    new Vector2(10, 10)
                }
            ),
            // 8
            new TestData(new[] {
                    new Vector2(-10, -10),
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10)
                }, new[] {
                    new[] {
                        new Vector2(5, 5),
                        new Vector2(-5, 5),
                        new Vector2(-5, -5),
                        new Vector2(5, -5)
                    }
                }, new[] {
                    new Vector2(-15, 15),
                    new Vector2(-15, -15),
                    new Vector2(15, -15),
                    new Vector2(15, 15)
                }
            ),
            // 9
            new TestData(new[] {
                    new Vector2(-10, -10),
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10)
                },
                new Vector2[0][],
                new[] {
                    new Vector2(-5, 15),
                    new Vector2(-5, -5),
                    new Vector2(5, -5),
                    new Vector2(5, 15)
                }
            ),
            // 10
            new TestData(new[] {
                    new Vector2(10, -15),
                    new Vector2(-5, -15),
                    new Vector2(-5, 10),
                    new Vector2(10, 10)
                }, new[] {
                    new[] {
                        new Vector2(5, -10),
                        new Vector2(5, -5),
                        new Vector2(0, -5),
                        new Vector2(0, -10)
                    }
                }, new[] {
                    new Vector2(0, 5),
                    new Vector2(-10, 5),
                    new Vector2(-10, 0),
                    new Vector2(0, 0)
                }
            ),
            // 11
            new TestData(new[] {
                    new Vector2(10, -15),
                    new Vector2(-5, -15),
                    new Vector2(-5, 10),
                    new Vector2(10, 10)
                }, new[] {
                    new[] {
                        new Vector2(5, -10),
                        new Vector2(5, -5),
                        new Vector2(0, -5),
                        new Vector2(0, -10)
                    }
                }, new[] {
                    new Vector2(15, 5),
                    new Vector2(-10, 5),
                    new Vector2(-10, 0),
                    new Vector2(15, 0)
                }
            ),
            // 12
            new TestData(new[] {
                    new Vector2(-20, 30),
                    new Vector2(20, 30),
                    new Vector2(20, -25),
                    new Vector2(-20, -25)
                }, new[] {
                    new[] {
                        new Vector2(-5, 25),
                        new Vector2(-5, 20),
                        new Vector2(5, 20),
                        new Vector2(5, 25)
                    },
                    new[] {
                        new Vector2(-15, 15),
                        new Vector2(-15, 10),
                        new Vector2(15, 10),
                        new Vector2(15, 15)
                    },
                    new[] {
                        new Vector2(-5, 5),
                        new Vector2(-5, 0),
                        new Vector2(5, 0),
                        new Vector2(5, 5)
                    },
                    new[] {
                        new Vector2(-15, -5),
                        new Vector2(-15, -10),
                        new Vector2(15, -10),
                        new Vector2(15, -5)
                    },
                    new[] {
                        new Vector2(-5, -15),
                        new Vector2(-5, -20),
                        new Vector2(5, -20),
                        new Vector2(5, -15)
                    }
                }, new[] {
                    new Vector2(10, 20),
                    new Vector2(10, -15),
                    new Vector2(25, -15),
                    new Vector2(25, 20)
                }
            ),
            // 13
            new TestData(new[] {
                    new Vector2(-20, 30),
                    new Vector2(20, 30),
                    new Vector2(20, -25),
                    new Vector2(-20, -25)
                }, new[] {
                    new[] {
                        new Vector2(-5, 25),
                        new Vector2(-5, 20),
                        new Vector2(5, 20),
                        new Vector2(5, 25)
                    },
                    new[] {
                        new Vector2(-15, 15),
                        new Vector2(-15, 10),
                        new Vector2(15, 10),
                        new Vector2(15, 15)
                    },
                    new[] {
                        new Vector2(-5, 5),
                        new Vector2(-5, 0),
                        new Vector2(5, 0),
                        new Vector2(5, 5)
                    },
                    new[] {
                        new Vector2(-15, -5),
                        new Vector2(-15, -10),
                        new Vector2(15, -10),
                        new Vector2(15, -5)
                    },
                    new[] {
                        new Vector2(-5, -15),
                        new Vector2(-5, -20),
                        new Vector2(5, -20),
                        new Vector2(5, -15)
                    }
                }, new[] {
                    new Vector2(-25, 10),
                    new Vector2(-25, -10),
                    new Vector2(25, -10),
                    new Vector2(25, 10)
                }
            ),
            // 14
            new TestData(new[] {
                    new Vector2(-20, 20),
                    new Vector2(20, 20),
                    new Vector2(20, -15),
                    new Vector2(-20, -15)
                }, new[] {
                    new[] {
                        new Vector2(-5, 5),
                        new Vector2(-5, 0),
                        new Vector2(5, 0),
                        new Vector2(5, 5)
                    },
                    new[] {
                        new Vector2(-15, -5),
                        new Vector2(-15, -10),
                        new Vector2(15, -10),
                        new Vector2(15, -5)
                    }
                }, new[] {
                    new Vector2(-5.5f, 2.5f),
                    new Vector2(25, -15),
                    new Vector2(25, 20)
                }
            ),
            // 15
            new TestData(new[] {
                    new Vector2(-20, 20),
                    new Vector2(20, 20),
                    new Vector2(20, -15),
                    new Vector2(-20, -15)
                }, new[] {
                    new[] {
                        new Vector2(-5, 5),
                        new Vector2(-5, 0),
                        new Vector2(5, 0),
                        new Vector2(5, 5)
                    },
                    new[] {
                        new Vector2(-15, -5),
                        new Vector2(-15, -10),
                        new Vector2(15, -10),
                        new Vector2(15, -5)
                    }
                }, new[] {
                    new Vector2(-5.5f, 2.5f),
                    new Vector2(25, -15),
                    new Vector2(0, 5)
                }
            ),
            // 16
            new TestData(new[] {
                    new Vector2(-5, 5),
                    new Vector2(-5, -5),
                    new Vector2(10, -5),
                    new Vector2(10, -10),
                    new Vector2(-10, -10),
                    new Vector2(-10, 10),
                    new Vector2(10, 10),
                    new Vector2(10, 5)
                },
                new Vector2[0][],
                new[] {
                    new Vector2(0, 15),
                    new Vector2(0, -15),
                    new Vector2(5, -15),
                    new Vector2(5, 15)
                }
            ),
            // 17
            new TestData(new[] {
                    new Vector2(-5, 5),
                    new Vector2(-5, -5),
                    new Vector2(10, -5),
                    new Vector2(10, -20),
                    new Vector2(-10, -20),
                    new Vector2(-10, 20),
                    new Vector2(10, 20),
                    new Vector2(10, 5)
                },
                new Vector2[0][],
                new[] {
                    new Vector2(0, 15),
                    new Vector2(0, -15),
                    new Vector2(5, -15),
                    new Vector2(5, 15)
                }
            ),
            // 18
            new TestData(new[] {
                    new Vector2(-5, 5),
                    new Vector2(-5, -5),
                    new Vector2(20, -5),
                    new Vector2(20, -25),
                    new Vector2(-10, -25),
                    new Vector2(-10, 25),
                    new Vector2(20, 25),
                    new Vector2(20, 5)
                }, new[] {
                    new[] {
                        new Vector2(-5, 15),
                        new Vector2(-5, 10),
                        new Vector2(10, 10),
                        new Vector2(10, 15)
                    },
                    new[] {
                        new Vector2(5, -10),
                        new Vector2(5, -15),
                        new Vector2(10, -15),
                        new Vector2(10, -10)
                    }
                }, new[] {
                    new Vector2(0, 20),
                    new Vector2(0, -20),
                    new Vector2(15, -20),
                    new Vector2(15, 20)
                }
            ),
            // 19
            new TestData(new[] {
                    new Vector2(-25, 20),
                    new Vector2(25, 20),
                    new Vector2(25, -20),
                    new Vector2(-25, -20)
                }, new[] {
                    new[] {
                        new Vector2(-20, 15),
                        new Vector2(-20, 5),
                        new Vector2(20, 5),
                        new Vector2(20, 15)
                    },
                    new[] {
                        new Vector2(-5, 0),
                        new Vector2(-5, -5),
                        new Vector2(5, -5),
                        new Vector2(5, 0)
                    }
                }, new[] {
                    new Vector2(-15, 10),
                    new Vector2(-15, -15),
                    new Vector2(15, -15),
                    new Vector2(15, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10),
                    new Vector2(-10, 10)
                }
            ),
            // 20
            new TestData(new[] {
                    new Vector2(-25, 30),
                    new Vector2(25, 30),
                    new Vector2(25, -30),
                    new Vector2(-25, -30)
                }, new[] {
                    new[] {
                        new Vector2(-20, 5),
                        new Vector2(-20, -5),
                        new Vector2(20, -5),
                        new Vector2(20, 5)
                    },
                    new[] {
                        new Vector2(-5, -10),
                        new Vector2(-5, -15),
                        new Vector2(5, -15),
                        new Vector2(5, -10)
                    },
                    new[] {
                        new Vector2(-5, 15),
                        new Vector2(-5, 10),
                        new Vector2(5, 10),
                        new Vector2(5, 15)
                    }
                }, new[] {
                    new Vector2(15, 5),
                    new Vector2(15, 25),
                    new Vector2(-15, 25),
                    new Vector2(-15, -25),
                    new Vector2(15, -25),
                    new Vector2(15, -5),
                    new Vector2(10, -5),
                    new Vector2(10, -20),
                    new Vector2(-10, -20),
                    new Vector2(-10, 20),
                    new Vector2(10, 20),
                    new Vector2(10, 5)
                }
            ),
            // 21
            new TestData(new[] {
                    new Vector2(-15, 10),
                    new Vector2(15, 10),
                    new Vector2(15, -10),
                    new Vector2(-15, -10)
                }, new[] {
                    new[] {
                        new Vector2(-5, 5),
                        new Vector2(-5, -5),
                        new Vector2(5, -5),
                        new Vector2(5, 5)
                    }
                }, new[] {
                    new Vector2(-10, 5),
                    new Vector2(-10, -5),
                    new Vector2(10, -5),
                    new Vector2(10, 5)
                }
            ),
            // 22
            new TestData(new[] {
                    new Vector2(-25, 20),
                    new Vector2(25, 20),
                    new Vector2(25, -20),
                    new Vector2(-25, -20)
                }, new[] {
                    new[] {
                        new Vector2(10, 0),
                        new Vector2(10, -5),
                        new Vector2(20, -5),
                        new Vector2(20, 0)
                    },
                    new[] {
                        new Vector2(10, -10),
                        new Vector2(10, -15),
                        new Vector2(20, -15),
                        new Vector2(20, -10)
                    },
                    new[] {
                        new Vector2(-20, 15),
                        new Vector2(-20, 5),
                        new Vector2(20, 5),
                        new Vector2(20, 15)
                    },
                    new[] {
                        new Vector2(-5, 0),
                        new Vector2(-5, -5),
                        new Vector2(5, -5),
                        new Vector2(5, 0)
                    },
                    new[] {
                        new Vector2(-20, 0),
                        new Vector2(-20, -5),
                        new Vector2(-10, -5),
                        new Vector2(-10, 0)
                    },
                    new[] {
                        new Vector2(-20, -10),
                        new Vector2(-20, -15),
                        new Vector2(-10, -15),
                        new Vector2(-10, -10)
                    },
                    new[] {
                        new Vector2(-5, -10),
                        new Vector2(-5, -15),
                        new Vector2(5, -15),
                        new Vector2(5, -10)
                    }
                }, new[] {
                    new Vector2(-15, 10),
                    new Vector2(-15, -15),
                    new Vector2(15, -15),
                    new Vector2(15, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10),
                    new Vector2(-10, 10)
                }
            ),
            // 23
            new TestData(new[] {
                    new Vector2(-25, 30),
                    new Vector2(25, 30),
                    new Vector2(25, -30),
                    new Vector2(-25, -30)
                }, new[] {
                    new[] {
                        new Vector2(-20, 5),
                        new Vector2(-20, -5),
                        new Vector2(20, -5),
                        new Vector2(20, 5)
                    },
                    new[] {
                        new Vector2(-5, -10),
                        new Vector2(-5, -15),
                        new Vector2(5, -15),
                        new Vector2(5, -10)
                    },
                    new[] {
                        new Vector2(-5, -20),
                        new Vector2(-5, -25),
                        new Vector2(5, -25),
                        new Vector2(5, -20)
                    },
                    new[] {
                        new Vector2(-15, -20),
                        new Vector2(-15, -25),
                        new Vector2(-10, -25),
                        new Vector2(-10, -20)
                    },
                    new[] {
                        new Vector2(10, -10),
                        new Vector2(10, -15),
                        new Vector2(20, -15),
                        new Vector2(20, -10)
                    },
                    new[] {
                        new Vector2(-5, 15),
                        new Vector2(-5, 10),
                        new Vector2(5, 10),
                        new Vector2(5, 15)
                    },
                    new[] {
                        new Vector2(-5, 25),
                        new Vector2(-5, 20),
                        new Vector2(5, 20),
                        new Vector2(5, 25)
                    },
                    new[] {
                        new Vector2(10, 25),
                        new Vector2(10, 20),
                        new Vector2(15, 20),
                        new Vector2(15, 25)
                    },
                    new[] {
                        new Vector2(-20, 15),
                        new Vector2(-20, 10),
                        new Vector2(-10, 10),
                        new Vector2(-10, 15)
                    }
                }, new[] {
                    new Vector2(15, 5),
                    new Vector2(15, 25),
                    new Vector2(-15, 25),
                    new Vector2(-15, -25),
                    new Vector2(15, -25),
                    new Vector2(15, -5),
                    new Vector2(10, -5),
                    new Vector2(10, -20),
                    new Vector2(-10, -20),
                    new Vector2(-10, 20),
                    new Vector2(10, 20),
                    new Vector2(10, 5)
                }
            ),
            // 24
            new TestData(new[] {
                    new Vector2(-25, 20),
                    new Vector2(25, 20),
                    new Vector2(25, -20),
                    new Vector2(-25, -20)
                }, new[] {
                    new[] {
                        new Vector2(-20, 15),
                        new Vector2(-20, 5),
                        new Vector2(20, 5),
                        new Vector2(20, 15)
                    },
                    new[] {
                        new Vector2(0, 0),
                        new Vector2(0, -5),
                        new Vector2(5, -5),
                        new Vector2(5, 0)
                    },
                    new[] {
                        new Vector2(-20, 0),
                        new Vector2(-20, -5),
                        new Vector2(-5, -5),
                        new Vector2(-5, 0)
                    },
                    new[] {
                        new Vector2(-5, -7.5f),
                        new Vector2(-5, -17.5f),
                        new Vector2(5, -17.5f),
                        new Vector2(5, -7.5f)
                    }
                }, new[] {
                    new Vector2(-15, 10),
                    new Vector2(-15, -15),
                    new Vector2(15, -15),
                    new Vector2(15, 10),
                    new Vector2(10, 10),
                    new Vector2(10, -10),
                    new Vector2(-10, -10),
                    new Vector2(-10, 10)
                }
            )
        };
    }

}