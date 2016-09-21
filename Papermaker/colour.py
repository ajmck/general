""" Functions to handle colours """

import random

def hsl(hue, saturation, lightness):
    """
    Converts provided values into a string for PIL.
    Not totally necessary, but I'm used to how Color works in C# / .Net
    :param hue: Hue (0-360)
    :param saturation: Saturation (0-100)
    :param lightness: lightness (0 being darkest, 100 being lightest, 50 being standard)
    :return string of HSL value used by Pillow:
    """
    return "hsl({},{}%,{}%)".format(hue, saturation, lightness)


def modify_hsl(hue, saturation, lightness, factor=25):
    """
    Randomly modifies the saturation and lightness, with an optional amount to modify the values by.
    :param hue: Hue
    :param saturation: Saturation
    :param lightness: Lightness
    :param factor: Maximum to modify the values by
    :return HSL string of randomised colour:
    """
    factor = random.randint(10, 100)
    # return "rgb({},{},{})".format(random.randint(0,255), random.randint(0,255), random.randint(0,255))
    return hsl(abs(hue), abs(saturation + random.randint(factor * -1, factor)), abs(lightness + random.randint(factor * -1, factor)))


def random_hsl():
    """
    Creates a new, randomly generated HSL value.
    :return: hue, saturation, lightness (in that order)
    """
    h = random.randint(0, 360)
    s = random.randint(0, 100)
    l = random.randint(0, 100)

    return h, s, l
