//MIT License

//Copyright(c) 2019 Bryce Tuton

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class to represent a complete Rubik's cube
/// </summary>
namespace Rubik_s_Cube {
    class Cube {
        public const int Size = 3;
        public enum Colours { green, yellow, red, white, orange, blue };
        public enum PieceTypes { centre, edge, corner };
        public enum Positions : byte { top = 0x1, bottom = 0x2, front = 0x4, back = 0x8, left = 0x10, right = 0x20 };
        public enum MoveType { column, row };

        // Sided within a cube, used to store which pieces are within what side
        private Piece[] top;
        private Piece[] bottom;
        private Piece[] front;
        private Piece[] back;
        private Piece[] right;
        private Piece[] left;

        // All the pieces of the cube
        private Centre[] centres;
        private Edge[] edges;
        private Corner[] corners;

        /// <summary>
        /// Standard constructor, used for created on Rubik's cube that is already solved
        /// </summary>
        public Cube() {
            int piecesInASide = Size * Size;

            top = new Piece[piecesInASide];
            bottom = new Piece[piecesInASide];
            front = new Piece[piecesInASide];
            back = new Piece[piecesInASide];
            right = new Piece[piecesInASide];
            left = new Piece[piecesInASide];

            centres = new Centre[6];

            // Create all the centre pieces then add them as the first item in each side
            centres[0] = new Centre(Colours.blue);
            bottom[0] = centres[0];
            centres[1] = new Centre(Colours.green);
            top[0] = centres[1];
            centres[2] = new Centre(Colours.orange);
            left[0] = centres[2];
            centres[3] = new Centre(Colours.red);
            right[0] = centres[3];
            centres[4] = new Centre(Colours.white);
            back[0] = centres[4];
            centres[5] = new Centre(Colours.yellow);
            front[0] = centres[5];

            edges = new Edge[12];

            // Create all the edge pieces that the cube could contain
            edges[0] = new Edge(new Colours[2] { Colours.green, Colours.yellow });
            edges[1] = new Edge(new Colours[2] { Colours.green, Colours.white });
            edges[2] = new Edge(new Colours[2] { Colours.green, Colours.red });
            edges[3] = new Edge(new Colours[2] { Colours.green, Colours.orange });
            edges[4] = new Edge(new Colours[2] { Colours.blue, Colours.yellow });
            edges[5] = new Edge(new Colours[2] { Colours.blue, Colours.white });
            edges[6] = new Edge(new Colours[2] { Colours.blue, Colours.red });
            edges[7] = new Edge(new Colours[2] { Colours.blue, Colours.orange });
            edges[8] = new Edge(new Colours[2] { Colours.yellow, Colours.red });
            edges[9] = new Edge(new Colours[2] { Colours.yellow, Colours.orange });
            edges[10] = new Edge(new Colours[2] { Colours.white, Colours.red });
            edges[11] = new Edge(new Colours[2] { Colours.white, Colours.orange });

            corners = new Corner[8];

            // Create all the corner pieces that the cube could contain
            corners[0] = new Corner(new Colours[3] { Colours.green, Colours.yellow, Colours.orange });
            corners[1] = new Corner(new Colours[3] { Colours.green, Colours.yellow, Colours.red });
            corners[2] = new Corner(new Colours[3] { Colours.green, Colours.white, Colours.orange });
            corners[3] = new Corner(new Colours[3] { Colours.green, Colours.white, Colours.red });
            corners[4] = new Corner(new Colours[3] { Colours.blue, Colours.yellow, Colours.orange });
            corners[5] = new Corner(new Colours[3] { Colours.blue, Colours.yellow, Colours.red });
            corners[6] = new Corner(new Colours[3] { Colours.blue, Colours.white, Colours.orange });
            corners[7] = new Corner(new Colours[3] { Colours.blue, Colours.white, Colours.red });

            UpdateSides();
        }

        /// <summary>
        /// Constructor used for creating a new cube that looks the same as the given cube
        /// </summary>
        /// <param name="cube">The cube base the new cube off</param>
        public Cube(Cube cube) {
            int piecesInASide = Size * Size;

            top = new Piece[piecesInASide];
            bottom = new Piece[piecesInASide];
            front = new Piece[piecesInASide];
            back = new Piece[piecesInASide];
            right = new Piece[piecesInASide];
            left = new Piece[piecesInASide];

            centres = new Centre[6];
            edges = new Edge[12];
            corners = new Corner[8];

            // Get the pieces of the other cube
            Centre[] otherCentres = cube.GetCentres();
            Edge[] otherEdges = cube.GetEdges();
            Corner[] otherCorners = cube.GetCorners();

            // Create each piece with the same colour as the pieces of the sample cube
            // and set the position to be the same as the sample cube
            for (int i = 0; i < centres.Length; i++) {
                centres[i] = new Centre(otherCentres[i].colour);
                centres[i].SetPosition(otherCentres[i].Position());
            }

            for (int i = 0; i < edges.Length; i++) {
                edges[i] = new Edge(otherEdges[i].GetColours());
                edges[i].SetPosition(otherEdges[i].GetPosition());
            }

            for (int i = 0; i < corners.Length; i++) {
                corners[i] = new Corner(otherCorners[i].GetColours());
                corners[i].SetPosition(otherCorners[i].GetPosition());
            }

            UpdateSides();
        }

        /// <summary>
        /// Constructor to create a cube with pieces in given positions
        /// </summary>
        /// <param name="positions">The positions to set the cube to</param>
        public Cube(Positions[] positions) {
            // Create cube like normal constructor
            int piecesInASide = Size * Size;

            top = new Piece[piecesInASide];
            bottom = new Piece[piecesInASide];
            front = new Piece[piecesInASide];
            back = new Piece[piecesInASide];
            right = new Piece[piecesInASide];
            left = new Piece[piecesInASide];

            centres = new Centre[6];

            centres[0] = new Centre(Colours.blue);
            bottom[0] = centres[0];
            centres[1] = new Centre(Colours.green);
            top[0] = centres[1];
            centres[2] = new Centre(Colours.orange);
            left[0] = centres[2];
            centres[3] = new Centre(Colours.red);
            right[0] = centres[3];
            centres[4] = new Centre(Colours.white);
            back[0] = centres[4];
            centres[5] = new Centre(Colours.yellow);
            front[0] = centres[5];

            edges = new Edge[12];

            edges[0] = new Edge(new Colours[2] { Colours.green, Colours.yellow });
            edges[1] = new Edge(new Colours[2] { Colours.green, Colours.white });
            edges[2] = new Edge(new Colours[2] { Colours.green, Colours.red });
            edges[3] = new Edge(new Colours[2] { Colours.green, Colours.orange });
            edges[4] = new Edge(new Colours[2] { Colours.blue, Colours.yellow });
            edges[5] = new Edge(new Colours[2] { Colours.blue, Colours.white });
            edges[6] = new Edge(new Colours[2] { Colours.blue, Colours.red });
            edges[7] = new Edge(new Colours[2] { Colours.blue, Colours.orange });
            edges[8] = new Edge(new Colours[2] { Colours.yellow, Colours.red });
            edges[9] = new Edge(new Colours[2] { Colours.yellow, Colours.orange });
            edges[10] = new Edge(new Colours[2] { Colours.white, Colours.red });
            edges[11] = new Edge(new Colours[2] { Colours.white, Colours.orange });

            corners = new Corner[8];

            corners[0] = new Corner(new Colours[3] { Colours.green, Colours.yellow, Colours.orange });
            corners[1] = new Corner(new Colours[3] { Colours.green, Colours.yellow, Colours.red });
            corners[2] = new Corner(new Colours[3] { Colours.green, Colours.white, Colours.orange });
            corners[3] = new Corner(new Colours[3] { Colours.green, Colours.white, Colours.red });
            corners[4] = new Corner(new Colours[3] { Colours.blue, Colours.yellow, Colours.orange });
            corners[5] = new Corner(new Colours[3] { Colours.blue, Colours.yellow, Colours.red });
            corners[6] = new Corner(new Colours[3] { Colours.blue, Colours.white, Colours.orange });
            corners[7] = new Corner(new Colours[3] { Colours.blue, Colours.white, Colours.red });

            // Instead of updating sides, set the positions of the cubes to match the given positions
            SetPositions(positions);
        }

        /// <summary>
        /// Method for setting the positions of all the pieces within the cube to match a given set
        /// of positions.
        /// </summary>
        /// <param name="positions">The positions to set the cube to</param>
        public void SetPositions(Positions[] positions) {
            for (int i = 0; i < centres.Length; i++) {
                centres[i].position = positions[i];
            }

            int index = 0;

            for (int i = 0; i < edges.Length * 2; i += 2) {
                Positions[] newPos = new Positions[2];
                newPos[0] = positions[i + centres.Length];
                newPos[1] = positions[i + centres.Length + 1];
                edges[index].SetPosition(newPos);
                index++;
            }

            index = 0;

            for (int i = 0; i < corners.Length * 3; i += 3) {
                Positions[] newPos = new Positions[3];
                newPos[0] = positions[i + centres.Length + edges.Length * 2];
                newPos[1] = positions[i + centres.Length + edges.Length * 2 + 1];
                newPos[2] = positions[i + centres.Length + edges.Length * 2 + 2];
                corners[index].SetPosition(newPos);
                index++;
            }

            UpdateSides();
        }

        /// <summary>
        /// Updates the sides of the cube so that each side only contains the pieces that are
        /// within that side
        /// </summary>
        private void UpdateSides() {
            int topIndex = 1;
            int bottomIndex = 1;
            int frontIndex = 1;
            int backIndex = 1;
            int rightIndex = 1;
            int leftIndex = 1;

            foreach (Edge edge in edges) {
                if (edge.InSide(Positions.top)) {
                    top[topIndex] = edge;
                    topIndex++;
                }
                if (edge.InSide(Positions.bottom)) {
                    bottom[bottomIndex] = edge;
                    bottomIndex++;
                }
                if (edge.InSide(Positions.front)) {
                    front[frontIndex] = edge;
                    frontIndex++;
                }
                if (edge.InSide(Positions.back)) {
                    back[backIndex] = edge;
                    backIndex++;
                }
                if (edge.InSide(Positions.right)) {
                    right[rightIndex] = edge;
                    rightIndex++;
                }
                if (edge.InSide(Positions.left)) {
                    left[leftIndex] = edge;
                    leftIndex++;
                }
            }

            foreach (Corner corner in corners) {
                if (corner.InSide(Positions.top)) {
                    top[topIndex] = corner;
                    topIndex++;
                }
                if (corner.InSide(Positions.bottom)) {
                    bottom[bottomIndex] = corner;
                    bottomIndex++;
                }
                if (corner.InSide(Positions.front)) {
                    front[frontIndex] = corner;
                    frontIndex++;
                }
                if (corner.InSide(Positions.back)) {
                    back[backIndex] = corner;
                    backIndex++;
                }
                if (corner.InSide(Positions.right)) {
                    right[rightIndex] = corner;
                    rightIndex++;
                }
                if (corner.InSide(Positions.left)) {
                    left[leftIndex] = corner;
                    leftIndex++;
                }
            }
        }

        /// <summary>
        /// Return the requested side of the cube
        /// </summary>
        /// <param name="sideName">The side of the cube to return</param>
        /// <returns>The requested cube side</returns>
        public Piece[] GetSide(string sideName) {
            if (sideName.ToLower().Equals("top"))
                return top;
            else if (sideName.ToLower().Equals("bottom"))
                return bottom;
            else if (sideName.ToLower().Equals("left"))
                return left;
            else if (sideName.ToLower().Equals("right"))
                return right;
            else if (sideName.ToLower().Equals("front"))
                return front;
            else
                return back;
        }

        /// <summary>
        /// Get the string representing the requested side of the cube. Used for creating
        /// a printable cube
        /// </summary>
        /// <param name="Side">The side to get the string of</param>
        /// <returns>String representing the side</returns>
        private string GetSideString(Piece[] Side) {
            // String to represent the sides colours.
            string[] pieces = new string[9]; 

            Positions position;
            Positions first, second, third, fourth;

            // Get the sides around the selected face
            if (Side.Equals(top)) {
                position = Positions.top;
                first = Positions.back;
                second = Positions.left;
                third = Positions.right;
                fourth = Positions.front;
            } else if (Side.Equals(bottom)) {
                position = Positions.bottom;
                first = Positions.front;
                second = Positions.left;
                third = Positions.right;
                fourth = Positions.back;
            } else if (Side.Equals(right)) {
                position = Positions.right;
                first = Positions.top;
                second = Positions.front;
                third = Positions.back;
                fourth = Positions.bottom;
            } else if (Side.Equals(left)) {
                position = Positions.left;
                first = Positions.top;
                second = Positions.back;
                third = Positions.front;
                fourth = Positions.bottom;
            } else if (Side.Equals(front)) {
                position = Positions.front;
                first = Positions.top;
                second = Positions.left;
                third = Positions.right;
                fourth = Positions.bottom;
            } else {
                position = Positions.back;
                first = Positions.bottom;
                second = Positions.left;
                third = Positions.right;
                fourth = Positions.top;
            }

            // Look through all the pieces within the side, find where they are in the cube,
            // then get their string code for that side and place it within the string array
            // depending where in the side they are located
            foreach (Piece piece in Side) {
                if (piece.GetPieceType().Equals(PieceTypes.centre)) {
                    Centre centre = piece as Centre;

                    pieces[4] = centre.GetColourString();
                } else if (piece.GetPieceType().Equals(PieceTypes.corner)) {
                    Corner corner = piece as Corner;

                    if (corner.InSide(first) && corner.InSide(second)) {
                        pieces[0] = corner.GetColourString(position);
                    } else if (corner.InSide(first) && corner.InSide(third)) {
                        pieces[2] = corner.GetColourString(position);
                    } else if (corner.InSide(fourth) && corner.InSide(second)) {
                        pieces[6] = corner.GetColourString(position);
                    } else {
                        pieces[8] = corner.GetColourString(position);
                    }
                } else if (piece.GetPieceType().Equals(PieceTypes.edge)) {
                    Edge edge = piece as Edge;

                    if (edge.InSide(first)) {
                        pieces[1] = edge.GetColourString(position);
                    } else if (edge.InSide(second)) {
                        pieces[3] = edge.GetColourString(position);
                    } else if (edge.InSide(third)) {
                        pieces[5] = edge.GetColourString(position);
                    } else {
                        pieces[7] = edge.GetColourString(position);
                    }
                }
            }

            // Transform string array to string then return
            string sideString = "";

            foreach (string colour in pieces) {
                sideString += colour;
            }

            return sideString;
        }

        /// <summary>
        /// Determines whether this cubes positions are the same as the given positions
        /// </summary>
        /// <param name="otherPositions">The positions to compare to</param>
        /// <returns>If cubes positons are the same as the given positions</returns>
        public bool SamePositions(Positions[] otherPositions) {
            bool same = true;
            Positions[] positions = GetPositions();

            for (int i = 0; i < positions.Length; i++)
                if (!positions[i].GetHashCode().Equals(otherPositions[i]))
                    same = false;

            return same;
        }

        /// <summary>
        /// Gets the current positions of the cube pieces
        /// </summary>
        /// <returns>Current positions</returns>
        public Positions[] GetPositions() {
            Positions[] pos = new Positions[54];

            for (int i = 0; i < centres.Length; i++) {
                pos[i] = centres[i].position;
            }

            int index = 0;

            for (int i = 0; i < edges.Length * 2; i += 2) {
                Positions[] cPos = edges[index].GetPosition();
                pos[i + centres.Length] = cPos[0];
                pos[i + centres.Length + 1] = cPos[1];
                index++;
            }

            index = 0;

            for (int i = 0; i < corners.Length * 3; i += 3) {
                Positions[] cPos = corners[index].GetPosition();
                pos[i + centres.Length + edges.Length * 2] = cPos[0];
                pos[i + centres.Length + edges.Length * 2 + 1] = cPos[1];
                pos[i + centres.Length + edges.Length * 2 + 2] = cPos[2];
                index++;
            }

            return pos;
        }

        /// <summary>
        /// Gets the current colours of the cube pieces
        /// </summary>
        /// <returns>Current colours</returns>
        public Colours[] GetColours() {
            Colours[] col = new Colours[54];

            for (int i = 0; i < centres.Length; i++) {
                col[i] = centres[i].colour;
            }

            int index = 0;

            for (int i = 0; i < edges.Length * 2; i += 2) {
                Colours[] cCol = edges[index].GetColours();
                col[i + centres.Length] = cCol[0];
                col[i + centres.Length + 1] = cCol[1];
                index++;
            }

            index = 0;

            for (int i = 0; i < corners.Length * 3; i += 3) {
                Colours[] cCol = corners[index].GetColours();
                col[i + centres.Length + edges.Length * 2] = cCol[0];
                col[i + centres.Length + edges.Length * 2 + 1] = cCol[1];
                col[i + centres.Length + edges.Length * 2 + 2] = cCol[2];
                index++;
            }

            return col;
        }

        /// <summary>
        /// Gets the corners of the cube
        /// </summary>
        /// <returns>Corners of the cube</returns>
        public Corner[] GetCorners() {
            return corners;
        }

        /// <summary>
        /// Gets the edges of the cube
        /// </summary>
        /// <returns>edges of the cube</returns>
        public Edge[] GetEdges() {
            return edges;
        }

        /// <summary>
        /// Gets the centres of the cube
        /// </summary>
        /// <returns>centres of the cube</returns>
        public Centre[] GetCentres() {
            return centres;
        }

        /// <summary>
        /// Gets the printable cube as a string
        /// </summary>
        /// <returns>String representing the cube</returns>
        public string[] GetCubeDisplay() {
            string[] sides = new string[6];

            sides[0] = GetSideString(top);
            sides[1] = GetSideString(front);
            sides[2] = GetSideString(left);
            sides[3] = GetSideString(bottom);
            sides[4] = GetSideString(right);
            sides[5] = GetSideString(back);

            return sides;
        }

        /// <summary>
        /// Method to rotate a side on the cube. Rotates the selected side clockwise or not (clockwise is relative to front face)
        /// </summary>
        /// <param name="side">The side of the cube to rotate</param>
        /// <param name="clockWise">Rotate the side clockwise?</param>
        public void RotateSide(Positions side, bool clockWise) {
            if (side.Equals(Positions.front)) {
                foreach (Piece piece in front) {
                    if (piece.GetPieceType() != PieceTypes.centre)
                        piece.RotateAroundSide(side, clockWise);
                }
            } else if (side.Equals(Positions.back)) {
                foreach (Piece piece in back) {
                    if (piece.GetPieceType() != PieceTypes.centre)
                        piece.RotateAroundSide(side, clockWise);
                }
            } else if (side.Equals(Positions.right)) {
                foreach (Piece piece in right) {
                    if (piece.GetPieceType() != PieceTypes.centre)
                        piece.RotateAroundSide(side, clockWise);
                }
            } else if (side.Equals(Positions.left)) {
                foreach (Piece piece in left) {
                    if (piece.GetPieceType() != PieceTypes.centre)
                        piece.RotateAroundSide(side, clockWise);
                }
            } else if (side.Equals(Positions.top)) {
                foreach (Piece piece in top) {
                    if (piece.GetPieceType() != PieceTypes.centre)
                        piece.RotateAroundSide(side, clockWise);
                }
            } else if (side.Equals(Positions.bottom)) {
                foreach (Piece piece in bottom) {
                    if (piece.GetPieceType() != PieceTypes.centre)
                        piece.RotateAroundSide(side, clockWise);
                }
            }

            UpdateSides();
        }
        /// <summary>
        /// Checks if the cube is currently solved. Checks by comparing the display cube to what it 
        /// would look like if the cube was solved.
        /// </summary>
        /// <returns>If the cube is solved</returns>
        public bool IsSolved() {
            string[] cube = GetCubeDisplay();
            bool topCorrect, bottomCorrect, leftCorrect, rightCorrect, frontCorrect, backCorrect;

            topCorrect = cube[0].Equals("ggggggggg");
            bottomCorrect = cube[3].Equals("bbbbbbbbb");
            leftCorrect = cube[2].Equals("ooooooooo");
            rightCorrect = cube[4].Equals("rrrrrrrrr");
            frontCorrect = cube[1].Equals("yyyyyyyyy");
            backCorrect = cube[5].Equals("wwwwwwwww");

            if (topCorrect && bottomCorrect && leftCorrect && rightCorrect && frontCorrect && backCorrect)
                return true;
            else
                return false;
        }
    }
}
