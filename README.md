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
