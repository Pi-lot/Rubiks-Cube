//MIT License

//Copyright(c) 2019 Bryce Tuton

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class that represents the Corner piece of the Rubik's cube
/// </summary>
namespace Rubik_s_Cube {
    class Corner : Piece {
        /// <summary>
        /// Constructor to create corner piece of specified colours
        /// </summary>
        /// <param name="colours">The colours the corner should be</param>
        public Corner(Cube.Colours[] colours) {
            colour = new Cube.Colours[3];
            position = new Cube.Positions[3];

            pieceType = Cube.PieceTypes.corner;

            for (int i = 0; i < 3; i++) {
                colour[i] = colours[i];

                switch (colour[i]) {
                    case Cube.Colours.green:
                        position[i] = Cube.Positions.top;
                        break;
                    case Cube.Colours.yellow:
                        position[i] = Cube.Positions.front;
                        break;
                    case Cube.Colours.red:
                        position[i] = Cube.Positions.right;
                        break;
                    case Cube.Colours.white:
                        position[i] = Cube.Positions.back;
                        break;
                    case Cube.Colours.orange:
                        position[i] = Cube.Positions.left;
                        break;
                    case Cube.Colours.blue:
                        position[i] = Cube.Positions.bottom;
                        break;
                }
            }
        }

        // Return position array
        public override Cube.Positions[] GetPosition() {
            return position;
        }

        // Return if piece is in the specified side
        public override bool InSide(Cube.Positions side) {
            bool inSide = false;

            foreach (Cube.Positions pos in position) {
                if (pos.Equals(side))
                    inSide = true;
            }

            return inSide;
        }
    }
}
