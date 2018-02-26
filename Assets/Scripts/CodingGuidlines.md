Guidelines for Coding
=====================

##Documentation
Documentation will be done similar to the [Qt](https://doc.qt.io/qt-5/qtwritingstyle-cpp.html) documentation style. Here is a general idea of how this will look:

```c++
/*! \brief Returns the distance between two points
 *
 * \param (Vec2) point1 - The first point
 * \param (Vec2) point2 - The second point
 *
 * \return (float) The distance between the points
 */
float distance(Vec2 point1, Vec2 point2)
{
    ...
}
```
The brief tag should just be a basic description. Other comments can be left throughout the code, especially in areas that seem complex.
In front of classes, documentation should look like:

```c++
/*! \class Vec2
 *  \brief Handles the creation of 2D vectors
 */
class Vec2
{
    ...
}
```

##Code Conventions
Variable names should be lower camel case, and function names should be upper camel case. Curly braces should also be on a new line. Enum variables and constants should be CAPITAL.
```c++
enum states { INGAME, PAUSE };

/*! \brief An example method
 */
void MethodName()
{
	int methodVar;
}
```
