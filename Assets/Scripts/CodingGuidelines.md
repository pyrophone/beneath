Guidelines for Coding
=====================
## SDKs
Currently, the only SDK used by the project is the Mapbox SDK for Unity version 1.3.0. This is already integrated into the Unity project on the GitHub repo.

## Documentation
Documentation will be done similar to the [Qt](https://doc.qt.io/qt-5/qtwritingstyle-cpp.html) documentation style. Here is a general idea of how this will look:

```c#
/*! \brief Returns the distance between two points
 *
 * \param (Vec2) point1 - The first point
 * \param (Vec2) point2 - The second point
 *
 * \return (float) The distance between the points
 */
public float distance(Vec2 point1, Vec2 point2)
{
    ...
}
```
The brief tag should just be a basic description. Other comments can be left throughout the code, especially in areas that seem complex or difficult to understand.
In front of classes, documentation should look like:

```c#
/*! \class Vec2
 *  \brief Handles the creation of 2D vectors
 */
public class Vec2
{
    ...
}
```

After a variable is decalred, there can be a brief description after if using inline documentation.
For example:

```c#
private int score //! The score of the player
```

## Code Conventions
Variable and file names should be lower camel case, and function and folder names should be upper camel case. Curly braces should also be on a new line. Enum variables and constants should be capitals. Please also use the this keyward infront of class members

```c#
private enum states { INGAME, PAUSE };
private string sampleString;

/*! \brief An example method
 */
public void MethodName(string sampleString)
{
	int methodVar = 9;
	this.sampleString = sampleString;
}
```

For more information on contributing, please read the [Contributing Guidelines](../../CONTRIBUTING.md).
