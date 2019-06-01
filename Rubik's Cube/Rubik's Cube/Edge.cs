using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class to represent the Edge piece of a Rubik's cube. Inherits Piece class
/// </summary>
namespace Rubik_s_Cube {
    class Edge : Piece {
        /// <summary>
        /// Contruct the Edge with given colours
        /// </summary>
        /// <param name="colours">The colours the piece will be</param>
        public Edge(Cube.Colours[] colours) {
            this.colour = new Cube.Colours[2];
            position = new Cube.Positions[2];

            pieceType = Cube.PieceTypes.edge;

            for (int i = 0; i < 2; i++) {
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

        // Return if in side
        public override bool InSide(Cube.Positions side) {
            foreach (Cube.Positions pos in position) {
                if (pos == side)
                    return true;
            }

            return false;
        }
    }
}
