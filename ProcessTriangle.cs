using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/// <summary>
/// Coding aspect in connection with employment application to Yodle.
/// </summary>
namespace YodleCareers
{
    public class ProcessTriangle
    {
        private static string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static FileInfo triangleDescription = new FileInfo(myDocPath + @"\yodle_triangle.txt");
        private static StringBuilder triangleBuilder = new StringBuilder();

        public static void Main(string[] args)
        {

            // Read in the description of the triange from yodle_triangle.txt
            GetYodleTriangle();

            try
            {
                YodleTriangle t = new YodleTriangle(triangleBuilder.ToString());
                Console.WriteLine(t.ToString());
                Console.WriteLine("The maximum total of triangle t: {0}", t.MaxTotal());
                Console.WriteLine("The minimum total of triangle t: {0}", t.MinTotal());
            } // END try

            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            } // END catch (ArgumentException ex)

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } // END catch (Exception ex)

            finally
            {
                // empty for now

            }  // END finally

            return;
            
        } // END public static void Main(string[] args)

        /// <summary>
        /// Gets the yodle triangle.
        /// </summary>
        private static void GetYodleTriangle()
        {
            try
            {
                using (StreamReader sr = new StreamReader(triangleDescription.FullName, true))
                {
                    triangleBuilder.Append(sr.ReadToEnd());                    
                }
            } // END try

            catch (OutOfMemoryException ex)
            {
                Console.WriteLine("There is insufficient memory to allocate a buffer for the returned string:");
                Console.WriteLine(ex.Message);

            } // END catch (OutOfMemoryException ex)

            catch (IOException ex)
            {
                Console.WriteLine("I/O error:");
                Console.WriteLine(ex.Message);

            } // END catch (IOException ex)

            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception:");
                Console.WriteLine(ex.Message);

            } // END catch (Exception ex)

            finally
            {  
                // empty for now ...

            }  // END finally

            return;

        } // END private static void GetYodleTriangle()

    } // END public class ProcessTriangle

}  // END namespace YodleCareers
