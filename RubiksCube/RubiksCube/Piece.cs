//MIT License

//Copyright(c) 2019 Bryce Tuton

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Base class used for creating a piece within the cube
/// </summary>
namespace RubiksCube {
    public abstract class Piece {
        protected Cube.Positions[] position;
        protected Cube.PieceTypes pieceType;
        protected Cube.Colours[] colour;

        /// <summary>
        /// Rotates the side requested in a clockwise or anticlockwise direction (clockwise is relative
        /// to the front face)
        /// Goes through and changes the positions of the pieces within the selected side then updates
        /// the cube.
        /// </summary>
        /// <param name="side">The side to be rotated</param>
        /// <param name="clockWise">Rotate clockwise?</param>
        public void RotateAroundSide(Cube.Positions side, bool clockWise) {
            if (side.Equals(Cube.Positions.front)) {
                for (int i = 0; i < position.Length; i++) {
                    if (clockWise) {
                        if (position[i] == Cube.Positions.left) {
                            position[i] = Cube.Positions.top;
                            continue;
                        } else if (position[i] == Cube.Positions.top) {
                            position[i] = Cube.Positions.right;
                            continue;
                        } else if (position[i] == Cube.Positions.right) {
                            position[i] = Cube.Positions.bottom;
                            continue;
                        } else if (position[i] == Cube.Positions.bottom) {
                            position[i] = Cube.Positions.left;
                            continue;
                        }
                    } else {
                        if (position[i] == Cube.Positions.left) {
                            position[i] = Cube.Positions.bottom;
                            continue;
                        } else if (position[i] == Cube.Positions.top) {
                            position[i] = Cube.Positions.left;
                            continue;
                        } else if (position[i] == Cube.Positions.right) {
                            position[i] = Cube.Positions.top;
                            continue;
                        } else if (position[i] == Cube.Positions.bottom) {
                            position[i] = Cube.Positions.right;
                            continue;
                        }
                    }
                }
            } else if (side.Equals(Cube.Positions.back)) {
                for (int i = 0; i < position.Length; i++) {
                    if (!clockWise) {
                        if (position[i] == Cube.Positions.left) {
                            position[i] = Cube.Positions.top;
                            continue;
                        } else if (position[i] == Cube.Positions.top) {
                            position[i] = Cube.Positions.right;
                            continue;
                        } else if (position[i] == Cube.Positions.right) {
                            position[i] = Cube.Positions.bottom;
                            continue;
                        } else if (position[i] == Cube.Positions.bottom) {
                            position[i] = Cube.Positions.left;
                            continue;
                        }
                    } else {
                        if (position[i] == Cube.Positions.left) {
                            position[i] = Cube.Positions.bottom;
                            continue;
                        } else if (position[i] == Cube.Positions.top) {
                            position[i] = Cube.Positions.left;
                            continue;
                        } else if (position[i] == Cube.Positions.right) {
                            position[i] = Cube.Positions.top;
                            continue;
                        } else if (position[i] == Cube.Positions.bottom) {
                            position[i] = Cube.Positions.right;
                            continue;
                        }
                    }
                }
            } else if (side.Equals(Cube.Positions.right)) {
                for (int i = 0; i < position.Length; i++) {
                    if (clockWise) {
                        if (position[i] == Cube.Positions.front) {
                            position[i] = Cube.Positions.top;
                            continue;
                        } else if (position[i] == Cube.Positions.top) {
                            position[i] = Cube.Positions.back;
                            continue;
                        } else if (position[i] == Cube.Positions.back) {
                            position[i] = Cube.Positions.bottom;
                            continue;
                        } else if (position[i] == Cube.Positions.bottom) {
                            position[i] = Cube.Positions.front;
                            continue;
                        }
                    } else {
                        if (position[i] == Cube.Positions.front) {
                            position[i] = Cube.Positions.bottom;
                            continue;
                        } else if (position[i] == Cube.Positions.top) {
                            position[i] = Cube.Positions.front;
                            continue;
                        } else if (position[i] == Cube.Positions.back) {
                            position[i] = Cube.Positions.top;
                            continue;
                        } else if (position[i] == Cube.Positions.bottom) {
                            position[i] = Cube.Positions.back;
                            continue;
                        }
                    }
                }
            } else if (side.Equals(Cube.Positions.left)) {
                for (int i = 0; i < position.Length; i++) {
                    if (!clockWise) {
                        if (position[i] == Cube.Positions.front) {
                            position[i] = Cube.Positions.top;
                            continue;
                        } else if (position[i] == Cube.Positions.top) {
                            position[i] = Cube.Positions.back;
                            continue;
                        } else if (position[i] == Cube.Positions.back) {
                            position[i] = Cube.Positions.bottom;
                            continue;
                        } else if (position[i] == Cube.Positions.bottom) {
                            position[i] = Cube.Positions.front;
                            continue;
                        }
                    } else {
                        if (position[i] == Cube.Positions.front) {
                            position[i] = Cube.Positions.bottom;
                            continue;
                        } else if (position[i] == Cube.Positions.top) {
                            position[i] = Cube.Positions.front;
                            continue;
                        } else if (position[i] == Cube.Positions.back) {
                            position[i] = Cube.Positions.top;
                            continue;
                        } else if (position[i] == Cube.Positions.bottom) {
                            position[i] = Cube.Positions.back;
                            continue;
                        }
                    }
                }
            } else if (side.Equals(Cube.Positions.top)) {
                for (int i = 0; i < position.Length; i++) {
                    if (clockWise) {
                        if (position[i] == Cube.Positions.front) {
                            position[i] = Cube.Positions.left;
                            continue;
                        } else if (position[i] == Cube.Positions.right) {
                            position[i] = Cube.Positions.front;
                            continue;
                        } else if (position[i] == Cube.Positions.back) {
                            position[i] = Cube.Positions.right;
                            continue;
                        } else if (position[i] == Cube.Positions.left) {
                            position[i] = Cube.Positions.back;
                            continue;
                        }
                    } else {
                        if (position[i] == Cube.Positions.front) {
                            position[i] = Cube.Positions.right;
                            continue;
                        } else if (position[i] == Cube.Positions.right) {
                            position[i] = Cube.Positions.back;
                            continue;
                        } else if (position[i] == Cube.Positions.back) {
                            position[i] = Cube.Positions.left;
                            continue;
                        } else if (position[i] == Cube.Positions.left) {
                            position[i] = Cube.Positions.front;
                            continue;
                        }
                    }
                }
            } else if (side.Equals(Cube.Positions.bottom)) {
                for (int i = 0; i < position.Length; i++) {
                    if (!clockWise) {
                        if (position[i] == Cube.Positions.front) {
                            position[i] = Cube.Positions.left;
                            continue;
                        } else if (position[i] == Cube.Positions.right) {
                            position[i] = Cube.Positions.front;
                            continue;
                        } else if (position[i] == Cube.Positions.back) {
                            position[i] = Cube.Positions.right;
                            continue;
                        } else if (position[i] == Cube.Positions.left) {
                            position[i] = Cube.Positions.back;
                            continue;
                        }
                    } else {
                        if (position[i] == Cube.Positions.front) {
                            position[i] = Cube.Positions.right;
                            continue;
                        } else if (position[i] == Cube.Positions.right) {
                            position[i] = Cube.Positions.back;
                            continue;
                        } else if (position[i] == Cube.Positions.back) {
                            position[i] = Cube.Positions.left;
                            continue;
                        } else if (position[i] == Cube.Positions.left) {
                            position[i] = Cube.Positions.front;
                            continue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check 
        /// </summary>
        /// <param name="side"></param>
        /// <returns>Whether in the side or not</returns>
        public abstract bool InSide(Cube.Positions side);

        /// <summary>
        /// Gets the type of piece this is
        /// </summary>
        /// <returns>Type of piece</returns>
        public Cube.PieceTypes GetPieceType() {
            return pieceType;
        }

        /// <summary>
        /// Gets the colours of the piece (Note: not implemented in the Center piece
        /// type. Use colour instead)
        /// </summary>
        /// <returns>Array that corresponds to the colours of the piece</returns>
        public Cube.Colours[] GetColours() {
            return colour;
        }

        /// <summary>
        /// Gets the positions the piece sit int (Note: not implemented in the Center piece
        /// type. Use Position() instead)
        /// </summary>
        /// <returns>Array that corresponds to the positions of the piece</returns>
        public abstract Cube.Positions[] GetPosition();

        /// <summary>
        /// Sets the position of the piece. Used when moving the piece, or when constructing
        /// a cube that isn't started solved
        /// </summary>
        /// <param name="newPosition">The new position array to set the piece to</param>
        public virtual void SetPosition(Cube.Positions[] newPosition) {
            position = newPosition;
        }

        /// <summary>
        /// Returns the colour in the specifed position as a letter (as a string). Used for
        /// making a printable cube
        /// </summary>
        /// <param name="pos">The position to get the string for</param>
        /// <returns>String that represents the colour at the position of the piece</returns>
        public string GetColourString(Cube.Positions pos) {
            for (int i = 0; i < position.Length; i++) {
                if (position[i] == pos) {
                    if (colour[i] == Cube.Colours.green) {
                        return "g";
                    } else if (colour[i] == Cube.Colours.blue) {
                        return "b";
                    } else if (colour[i] == Cube.Colours.yellow) {
                        return "y";
                    } else if (colour[i] == Cube.Colours.white) {
                        return "w";
                    } else if (colour[i] == Cube.Colours.orange) {
                        return "o";
                    } else {
                        return "r";
                    }
                }
            }

            return null;
        }
    }
}
