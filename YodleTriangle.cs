using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Coding in connection with Yodle employment application.
/// 
/// 1 - Triangle
/// Triangle
/// By starting at the top of the triangle and moving to adjacent numbers on the row below, the maximum total from top to bottom is 27.
/// 5
/// 9  6
/// 4   6  8
/// 0   7  1   5
/// I.e. 5 + 9 + 6 + 7 = 27.
/// Write a program in a language of your choice to find the maximum total from top to bottom in triangle.txt, a text file containing a triangle with 100 rows. Send your solution and resume to [123456 AT yodle dot com], replacing 123456 with the maximum sum for the triangle.
/// 
/// http://www.yodlecareers.com/puzzles/triangle.html
/// </summary>
namespace YodleCareers
{
    /// <summary>
    /// 1 - Triangle
    /// Triangle
    /// By starting at the top of the triangle and moving to adjacent numbers on the row below,
    /// the maximum total from top to bottom is 27.
    /// 5
    /// 9   6
    /// 4  6  8
    /// 0  7  1  5
    /// i.e., 5 + 9 + 6 + 7 = 27.
    /// Write a program in a language of your choice to find the maximum total from top to
    /// bottom in triangle.txt, a text file containing a triangle with 100 rows. Send your
    /// solution and resume to [123456 AT yodle dot com], replacing 123456 with the maximum
    /// sum for the triangle.
    /// </summary>
    public class YodleTriangle
    {     
        // FIELDS

        /// <summary>
        /// The representation of a YodleTrianle as a int[][].  
        /// </summary>
        private int[][] _triangle;        

        /// <summary>
        /// The the result of MaxTotal().
        /// </summary>
        private int _maxTotal;

        /// <summary>
        /// The the result of MinTotal().
        /// </summary>
        private int _minTotal;               

        // CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="YodleTriangle"/> class.
        /// </summary>
        /// <param name="height">The height.  
        /// The height must be a positive integer greater than 0 (e.g., 100).
        /// </param>
        public YodleTriangle(int height=1)
        {
            // throw an exception if YodleTriangle height is less than 1.
            if (height < 1) 
                throw new System.ArgumentException("The height of the YodleTree may not be less than 1.", "height");
           
            this.Init(height);           

        }  // END public YodleTriangle(int height)

        /// <summary>
        /// Initializes a new instance of the <see cref="YodleTriangle"/> class.
        /// </summary>
        /// <param name="triangleDescription">A string describing YodleTriangle such that each row of the
        /// triangle is on a separate line</param>
        /// <example>
        /// 1 (\n)
        /// 2 3 (\n)
        /// 4 5 6 (\n)
        /// 7 8 9 10</example>
        /// <exception cref="System.ArgumentException">The triangle may not be empty;triangle</exception>
        public YodleTriangle(string triangleDescription)
        {
            //  Make sure that triangleDescription is not describing and empty triangle.
            if (string.IsNullOrEmpty(triangleDescription) || string.IsNullOrWhiteSpace(triangleDescription))
                throw new System.ArgumentException("The triangle may not be empty", "triangle");

            // count the number of NewLines to compute the Height of
            // the triangle represented by triangleDescription
            // aad 1 to account for the Lack of NewLine at the end of triangleDescription
            int triangleHeight = triangleDescription.Count(f => (f == '\n')) + 1;

            // create a triangle with triangleHeight
            this.Triangle = new int[triangleHeight][];

            // delimeters for parsing the contents of triangleDescription            
            char[] separator = {' ', '\n', '\r'};            

            // split triangleDescription using separators ' ', '\n' and '\r' and get rid of empties
            string[] td = triangleDescription.ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);                      

            int nextItem = 0;  // the next item in the td string array
            int nextRef = 0; // the next reference in this.Triangle, this.Triangle[nextRef]
           
            //  Loop while there are elements to add to Triangle from td[nextItem]
            while (nextItem < td.Length)
            {
                // the number of elements in each inner array is always nextRef+1
                // where nextRef = 0,1,2,3 . . .
                // e.g., nextRef = 0 ==> 1 element in the inner array @ Triangle[nextRef]
                //       nextRef = 1 ==> 2 elements in the inner array @ Triangle[nextRef]
                //       nextREf = 2 ==> 3 elements in the inner array @ Triange[nextRef] . . .
                this.Triangle[nextRef] = new int[nextRef + 1];               

                // Convert the string in td[nextItem++] to an int and place it in the Triangle[nextRef][col]
                for (int col = 0; col <= nextRef; col++)
                    this.Triangle[nextRef][col] = Convert.ToInt32(td[nextItem++]);

                // move to the next element that needs to be filled in Triangle
                nextRef++;

            } // END  while (nextItem < td.Length)  

        } // END public YodleTriangle(string[] triangle)

        /// <summary>
        /// Copy constructor that initializes a new instance of the <see cref="YodleTriangle"/> class.
        /// </summary>
        /// <param name="t">An instrance of a YodleTriangle.</param>
        public YodleTriangle(YodleTriangle t)
        {
            // Makce sure that t is not null
            if (t == null)
                throw new ArgumentNullException("t", "public YodleTriangle(YodleTriangle t): YodleTriangle may not be null.");

            // perform initial
            this.Min = t.Min;
            this.Max = t.Max;
            this.Triangle = new int[t.Height][];

            try
            {
                for (int row = 0; row < t.Height; row++)
                {
                    this.Triangle[row] = new int[t.Triangle[row].Length];

                    for (int col = 0; col < t.Triangle[row].Length; col++)
                    {
                        this.Triangle[row][col] = t.Triangle[row][col];

                    } // END for (int col = 0; col < t.Triangle[row].Length; col++)

                } // END for(int row=0; row < t.Height; row++)
            
            }  // END try
            
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Index out of range: {0}", ex.Message);
            
            }  // END catch (IndexOutOfRangeException ex)
            
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected exception occured: {0}", ex.Message);
            
            }  // END catch (Exception ex)

        } // END public YodleTriangle(YodleTriangle t)

        /// <summary>
        /// Creates each row of the YodleTriangle and
        /// initializes each element with a Random positive integer
        /// within the following Range: [YodleTriangle.MIN_RANDOM,YodleTriangle.MAX_RANDOM]
        /// </summary>
        /// <param name="height">The height.</param>
        private void Init(int height)
        {
            // initialize _triange, _min, _max
            Triangle = new int[height][];
            this.Max = int.MaxValue;
            this.Min = int.MinValue;

            // get a random number generator
            Random r = new System.Random();
            
            try
            {
                // create the YodleTriangle with given height
                for (int nextRow = 0; nextRow < Triangle.Length; nextRow++)
                {
                    // create the next row of elements
                    // each row has length of nextRow+1 where nextRow = 0, 1, 2, 3, . . . 
                    // if nextRow = 0, ==> _triangle[0].Length = 1
                    // if nextRow = 1; ==> _triangle[1].Length = 2
                    // . . .
                    Triangle[nextRow] = new int[nextRow + 1];

                    // initialize the next element of each row of the triangle with a 
                    // random number chosen from the following Range: [YodleConstants.MIN_RANDOM,YodleConstants.MAX_RANDOM]
                    for (int nextElement = 0; nextElement < Triangle[nextRow].Length; nextElement++)
                    {
                        Triangle[nextRow][nextElement] = r.Next(YodleConstants.MIN_RANDOM, YodleConstants.MAX_RANDOM);

                    } // End for (int nextElement = 0; nextElement < Triangle[nextRow].Length; nextElement++)

                } // END for (int nextRow = 0; nextRow < Triangle.Length; nextRow++)
            
            }  // END try
           
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Index out of range: {0}", ex.Message);

            } // END catch (IndexOutOfRangeException ex)
            
            catch (Exception ex)
            {
                Console.WriteLine("Unxpect exception occured: {0}", ex.Message);

            }  // END catch (Exception ex)

            return;

        } // END private void Init(void)

        // METHODS

        /// <summary>
        /// Gets the height in YodleTriangle
        /// The height is the number of rows from top to bottom (or bottom to top)
        /// in YodleTriangle.
        /// </summary>
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height
        {
            get { return Triangle.Length; } // END get;            

        }  // END public int Height

        /// <summary>
        /// Gets or sets the triangle array.
        /// </summary>
        /// <value>
        /// The triangle.
        /// </value>
        private int[][] Triangle
        {
            get { return this._triangle; }
            set { this._triangle = value; }

        } // private int[][] Triangle

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        private int Max
        {
            get { return this._maxTotal; }
            set { this._maxTotal = value; }

        } // END private int Max

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        private int Min
        {
            get { return this._minTotal; }
            set { this._minTotal = value; }

        } // END private int Min

        /// <summary>
        /// Gets the element at the very top of YodleTriangle
        /// </summary>        
        public int Top
        {
            get { return this.Triangle[0][0]; }

        }  // END public int Top       

        /// <summary>
        /// Finds the maximum total from top to bottom.
        /// </summary>
        /// <returns></returns>
        public int MaxTotal()
        {
            // If only 1 element is present in the triangle than no calculations are needed
            if (this.IsTiny())
                return this.Top;           
            
            // Else the triangle has more than one element
            // Make a deep copy of this triangle and dynamically compute the maximum total
            YodleTriangle temp = new YodleTriangle(this);

            int nextRow = 0;  // The next reference to the inner array Triangl[nextRow][]
            int nextCol = 0;  // The next reference within the inner array Triangle[][nextCol]

            // there is a possiblity of going outside the bonds of this.Triangle[][]
            try
            {
                for (nextRow = temp.Triangle.Length - 2; nextRow >= 0; nextRow--)
                    for (nextCol = 0; nextCol <= nextRow; nextCol++)
                        temp.Triangle[nextRow][nextCol] += Math.Max(temp.Triangle[nextRow + 1][nextCol],
                                                                    temp.Triangle[nextRow + 1][nextCol + 1]);

                this.Max = temp.Triangle[nextRow + 1][nextCol - 1];

            } // END try

            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);

            }  // END catch (IndexOutOfRangeException ex)

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            } // END catch (Exception ex)

            return this.Max;
           
        } // END public int MaxTotal()

        /// <summary>
        /// Finds the minimum total from top to bottom.
        /// </summary>
        /// <returns></returns>
        public int MinTotal()
        {
            // If only 1 element is present in the triangle than no calculations are needed
            if (this.IsTiny())
                return Triangle[0][0];

            // Else the triangle has more than one element
            // Make a deep copy of this triangle and dynamically compute the minimum total
            YodleTriangle temp = new YodleTriangle(this);

            int nextRow = 0;  // The next reference to the inner array Triangl[nextRow][]
            int nextCol = 0;  // The next reference within the inner array Triangle[][nextCol]
            
            // there is a possiblity of going outside the bonds of this.Triangle[][]
            try
            {
                for (nextRow = temp.Triangle.Length - 2; nextRow >= 0; nextRow--)
                    for (nextCol = 0; nextCol <= nextRow; nextCol++)
                        temp.Triangle[nextRow][nextCol] += Math.Min(temp.Triangle[nextRow + 1][nextCol],
                                                                    temp.Triangle[nextRow + 1][nextCol + 1]);

                this.Min = temp.Triangle[nextRow + 1][nextCol - 1];

            } // try

            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            } // END catch (IndexOutOfRangeException ex)

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } // END catch (Exception ex)

            return this.Min;

        } // END public int MinTotal()

        /// <summary>
        /// Gets the number of items/elements in YodelTriangle.  The number of items/elements in the
        /// triangle is calculated by f(x)=(x(x+1))/2 where x is a positive integer.
        /// There cannot be a triangle without at least one element.
        /// 
        /// e.g.  x = 1. Triangle has one row and one element
        /// e.g.  x != 2 because otherwise the triangle will be missing a corner
        /// e.g., x = 3  Triangle has 2 rows and 3 elements
        /// e.g., x != 4 because otherwise the triangle will be missing a corner
        ///  
        /// </summary>
        /// <value>
        /// The item count.
        /// </value>
        public int ItemCount
        {
            get { return ((this.Height*(this.Height + 1)) / 2); }  // END get;            

        }  // END public int ItemCount                

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents a YodleTriangle.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents a YodleTriangle.
        /// </returns>
        public override string ToString()
        {
            StringBuilder triangleString = new StringBuilder(null);  // buffer representation of YodelTriangle

            // Iterate through the triangle and append the elements
            // of each triangle row on a new line
            // format as follows
            // row_x: element_x element_x+1 element_x+2 . . . 
            // e.g., 1: 1111 2343 24323432 23423424           
            foreach (int[] row in Triangle)
            {
                triangleString.Append(row.Length + ":");

                foreach (int element in row)
                    triangleString.Append(" " + element);

                triangleString.Append(Environment.NewLine);
            
            } // END foreach (int[] row in Triangle)

            // remove the last occurence of a NewLine "\n"
            triangleString = triangleString.Remove(triangleString.ToString().LastIndexOf(Environment.NewLine), 
                                                                             Environment.NewLine.Length);            
            return triangleString.ToString();

        } // END public override string ToString()

        /// <summary>
        /// Determines whether this is an instance of a YodleTriangle that only has one row.
        /// </summary>
        /// <returns></returns>
        private bool IsTiny()
        {
            return (this.Height == 1);

        }  // END private bool IsTiny

    }  // END class YodleTriangle

}  //  END namespace Yodecareers