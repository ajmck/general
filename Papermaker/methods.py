import random

""" Different methods for generating random imagery """


def gen_vertStrips(size):
    return __vertStrips__(0, 0, size[0], size[1])


def __vertStrips__(minX, minY, maxX, maxY):
    points = []

    p1 = (0, 0)
    p2 = (0, 45)
    p3 = (45, 0)

    while p2[1] < maxY:
        while p1[0] < maxX:
            points.append((p1, p2, p3))
            p1 = p2
            p2 = p3
            p3 = (p1[0] + 45, p1[1])
            # print p1, p2, p3
        # print "New Row"
        p1 = (0, p1[1])
        p2 = (0, p2[1] + 45)
        p3 = (45, p3[1])

    return points


def gen_squareangles(size):
    return __squareangles__(0, 0, size[0], size[1])


def __squareangles__(minX, minY, maxX, maxY):
    points = []
    factor = 50

    p1 = (0, 0)
    p2 = (0, factor)
    p3 = (factor, 0)

    height = factor

    while height < maxY + factor:
        while p1[0] < maxX + factor:
            points.append((p1, p2, p3))
            p1 = p2
            p2 = p3
            p3 = (p1[0] + factor, p1[1])
            # print p1, p2, p3
        # print "New Row"
        p1 = (0, height)
        p2 = (0, height + factor)
        p3 = (factor, height)

        height += factor

    return points


def gen_centred(size):
    return __centred__(0, 0, size[0], size[1])


def __centred__(minX, minY, maxX, maxY):
    points = []

    centroid = (random.randint(0, maxX), random.randint(0, maxY))
    horz = 0
    vert = 0

    N, E, S, W = False, False, False, False
    p1 = (0, 0)

    while not (N and E and S and W):
        # # old format before refactoring (pseudocode)
        # # essentially, go around the edge of the picture
        # # when hitting an edge, do the next side

        # if not (direction)
        #     N and S change horizontal
        #     E and W change vertical
        #
        #     S and W change by a negative value
        #     if (horz or vert) > respective size
        #         horz or vert = edge (0 or respective size)

        if not N:
            horz += random.randint(20, 200)

            if horz > maxX:
                horz = maxX
                N = True
                # north done

        elif not E:
            vert += random.randint(20, 200)

            if vert > maxY:
                vert = maxY
                E = True
                # east done
        elif not S:
            horz -= random.randint(20, 200)

            if horz < 0:
                horz = 0
                S = True
                # south done
        elif not W:
            vert -= random.randint(20, 200)

            if vert < 0:
                vert = 0
                W = True



        p2 = (horz, vert)
        points.append((p1, p2, centroid))
        p1 = p2

    return points


def gen_new(size):
    return __gNew__(0, 0, size[0], size[1])


def __gNew__(minX, minY, maxX, maxY):
    points = []
    r = random.randint(3, 100)
    p1 = (0, 0)
    p2 = (0, r)
    p3 = (r, 0)

    height = r

    while height < maxY:
        while p1[0] < maxX:
            points.append((p1, p2, p3))
            p1 = p2
            p2 = p3
            p3 = (p1[0] + random.randint(3, 100), p1[1])
            # print p1, p2, p3
        # print "New Row"
        p1 = (0, height)
        p2 = (0, height + random.randint(3, 100))
        p3 = (45, height)

        height += random.randint(3, 100)

    return points


def gen_2(size):
    return __g2__(0, 0, size[0], size[1])


def __g2__(minX, minY, maxX, maxY):
    points = []
    x = random.randint(0, 100)
    p1 = (0, 0)
    p2 = (0, x)
    p3 = (x, 0)
    p4 = (random.randint(0, 100), random.randint(0, 100))

    height = x

    while height < maxY + x:
        while p1[0] < maxX:
            points.append((p1, p2, p3, p4))
            p1 = p2
            p2 = p3
            p3 = p4
            p4 = (p1[0] + random.randint(0, 100), p1[1])
            # print p1, p2, p3
        # print "New Row"
        p1 = (0, height)
        p2 = (0, height + random.randint(0, 100))
        p3 = (x, height)
        p4 = (height + random.randint(0, 100), height + random.randint(0, 100))

        height += random.randint(0, 100)

    return points


def gen_3(size):
    return __g3__(0, 0, size[0], size[1])


def __g3__(minX, minY, maxX, maxY):
    points = []
    x = random.randint(0, 100)
    p1 = (0, 0)
    p2 = (0, x)
    p3 = (x, 0)
    p4 = (random.randint(0, 100), random.randint(0, 100))

    height = x

    while height < maxY + x:
        while p1[0] < maxX:
            points.append((p1, p2, p3, p4))
            p1 = p2
            p2 = p3
            p3 = p4
            p4 = (p1[0] + random.randint(0, 100), p1[1])
            # print p1, p2, p3
        # print "New Row"
        p1 = (0, height)
        p2 = (0, height + random.randint(0, 100))
        p3 = (x, height)
        p4 = (height + random.randint(0, 100), height + random.randint(0, 100))

        height += random.randint(0, 100)

    return points
