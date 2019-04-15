/* GeneratorType.cs
 * Programmer: Zach Sugano
 * Description: Enumeration of all of the different level generators
 * that can be invoked. Each enum as attached functions that can be
 * used to get a new generator of the requested type. For example:
 * 
 * Generator generator = GeneratorType.TFRACTAL.GetGenerator();
 * var rooms = generator.GetRooms();
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents which level generation pattern that we are going to use.
/// </summary>
public enum GeneratorType
{
    LINEAR, RANDOM, SQUARE, TFRACTAL, LINEAR_TEST
}

public static class GeneratorTypeMethods {

	public static Generator GetGenerator(this GeneratorType generatorType) {
		switch (generatorType)
        {
            case GeneratorType.LINEAR:
                return new GeneratorLinear();
            case GeneratorType.RANDOM:
                return new GeneratorRandom();
            case GeneratorType.TFRACTAL:
                return new GeneratorTFractal();
	    	case GeneratorType.LINEAR_TEST:
				return new GeneratorLinearTest();
            default:
                return new Generator();
        }
	}

}