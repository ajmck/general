import PIL.Image as Image
import PIL.ImageDraw as ImageDraw
import datetime
import random
import math

# Custom methods
from methods import gen_squareangles
from colour import *

# filename = random.randint(10000000, 99999999)
size = (2560, 1600)  # width, height


def create(points, color=None):
    if color is not None:
        h, s, l = color
    else:
        h, s, l = random_hsl()

    img = Image.new("RGB", size, hsl(h, s, l))

    draw = ImageDraw.Draw(img)

    for p in points:
        draw.polygon(p, modify_hsl(h, s, l))
    # img.show
    img.save(str(datetime.datetime.now()) + ".PNG", "PNG")


def __main__():
    for i in range(10):
        create(gen_squareangles(), color=(random.randint(0, 360), random.randint(0, 100), random.randint(0, 25)))
        print (i)


__main__()
