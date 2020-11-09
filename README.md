E3 programming

Omkar Bhagwat ombhagwat@gmail.com (for any questions feel free to contanct)

Please use this Visual Studio solution for your own reference. It is recommended that students would change code accordinly in their own visual studio solution to move forward in upcoming classes In case you are not able to build solution(or run the program) for your own project, please let me know .

Week 1 10.16.2020:

Topics Covered:

1. Classes/Objects (members, functions) in C#
2. How to setup a project in Visual Studio
3. Build Own custom logic (like PointInfo on-going)
4. Use Custom logic in Rhino (on-going)

Task To Try out for Week 1:
Define new method/methods for creating points randomly/diagonaly/in XY Grid/in XYZ Grid

a. Method to get Distance between 2 points (fromula to use https://www.mathsisfun.com/algebra/distance-2-points.html )
....try finding C# syntax for mathematical functions (like square root, square,Pi, etc.) yourself to implement the formula in method. (jusr google it).

b. Create new class in new file in Visula studio for "Window" which has Members/Properties/Attributes that you can think are realted to windows and
create Funtions/Methods (not implementation of those functions).

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Week 2 10.23.2020:

Topics Covered

1. Read and Write to files 
2. Rhino Project (create rhino commands)
3. Introduction RhinoCommon API

Task To Try out for week 2
Create Rhino Command that ask user for point count in X-direction (int is input) and Y-direction (int is input),
also ask user for spacing between points (double is input). Upon valid inputs given generate grid of points. 
(Hints: Use Rhino.Gemetry.Point3d class to create point Use total two "for loops" one for x direction and y direction nested in eachother.
Use doc.Objects.AddPoint() method to bake points in Rhino viewport )


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Week 3 - 10.30.2020:

Topics Covered


Task To Try out for week 3
1) Generate "N" number of random lines in given domain (domain is basically x dir/y dir/z dir range). 
Upon generating random lines successfully, user shall able to export the data in .csv file, also the color(RGB values) for each line

data structure of .csv File
eg. see attached csv


Hints
1) line consists of 2 points, start point and end point. If user needs 20 lines that means algorithm shall generate 20 x 2 number of points 
2) Generate random value for X Y Z co-ordinate for each point, create point with this values and add to list of points.

3) There can be multiple ways to create lines from the generated points, few options mentioned below.	

			
	option A Random points selection:  select one random point from the list and select another random point from the list. 
					   Connect these 2 points to form a line, before next iteration starts
					   remove these selected points from the list so that they wont repeat while creating new line.

	option B closest point selection:  Select one point from the list find the closest point which does not include the current point.
					   select these 2 points to form a line, and remove these 2 points from the main list so these points wont repeate.
