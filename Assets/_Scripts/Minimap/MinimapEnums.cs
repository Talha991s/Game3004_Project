/*  Author: Joseph Malibiran
 *  Date Created: January 28, 2021
 *  Last Updated: January 29, 2021
 *  Description: Manages and also retains information regarding the loaded save files and all available save files. 
 *  
 */

using System.Collections;
using System.Collections.Generic;

enum MinimapMarker
{
    NONE = 0,
    PLAYER = 1,
    ENEMY = 2,
    PICKUP = 3,
    CHECKPOINT = 4,
    HAZARD = 5
}
