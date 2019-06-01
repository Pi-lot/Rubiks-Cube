//MIT License

//Copyright(c) 2019 Bryce Tuton

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class that represents the Center piece of the Rubik's cube
/// </summary>
namespace RubiksCube {
    class Centre : Piece {
        public new Cube.Positions position;
        public new Cube.Colours colour;

        /// <summary>
        /// Constructor of Centre to create a centre of specified colour
        /// </summary>
        /// <param name="colour">The colour the piece will be</param>
        public Centre(Cube.Colours colour) {
            this.colour = colour;

            pieceType = (int)Cube.PieceTypes.centre;

            switch (colour) {
                case Cube.Colours.green:
                    position = Cube.Positions.top;
                    break;
                case Cube.Colours.yellow:
                    position = Cube.Positions.front;
                    break;
                case Cube.Colours.red:
                    position = Cube.Positions.right;
                    break;
                case Cube.Colours.white:
                    position = Cube.Positions.back;
                    break;
                case Cube.Colours.orange:
                    position = Cube.Positions.left;
                    break;
                case Cube.Colours.blue:
                    position = Cube.Positions.bottom;
                    break;
            }
        }

        /// <summary>
        /// Returns the colour of the piece as a string instead of enum
        /// </summary>
        /// <returns>String representing the colour of the piece</returns>
        internal string GetColourString() {
            if (colour == Cube.Colours.green) {
                return "g";
            } else if (colour == Cube.Colours.blue) {
                return "b";
            } else if (colour == Cube.Colours.yellow) {
                return "y";
            } else if (colour == Cube.Colours.white) {
                return "w";
            } else if (colour == Cube.Colours.orange) {
                return "o";
            } else {
                return "r";
            }
        }

        /// <summary>
        /// Method to get the position of the Centre piece (Used instead of GetPosition())
        /// </summary>
        /// <returns>Position of the centre</returns>
        public Cube.Positions Position() {
            return position;
        }

        // Return if piece is within the specified side
        public override bool InSide(Cube.Positions side) {
            if (position == side)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method inherited from Piece class. Not implemented for this class type, use Position() instead
        /// </summary>
        /// <returns></returns>
        public override Cube.Positions[] GetPosition() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the position of the piece. Used for moving the piece, or for creating a cube that
        /// isn't already solved
        /// </summary>
        /// <param name="newPosition">The new position to set to</param>
        public void SetPosition(Cube.Positions newPosition) {
            position = newPosition;
        }

        /// <summary>
        /// Method to move piece around a side. Does nothing since the centre piece type doesn't move
        /// </summary>
        public void RotateAroundSide() {

        }
    }
}
