import random
size = (2560, 1600)  # width, height

""" Different methods for generating random imagery """


def gen_vertStrips():
    points = []

    p1 = (0, 0)
    p2 = (0, 45)
    p3 = (45, 0)

    while p2[1] < size[1]:
        while p1[0] < size[0]:
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


def gen_squareangles():
    points = []
    factor = 50

    p1 = (0, 0)
    p2 = (0, factor)
    p3 = (factor, 0)

    height = factor

    while height < size[1] + factor:
        while p1[0] < size[0] + factor:
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


def gen_centred():
    points = []

    centroid = (random.randint(0, size[0]), random.randint(0, size[1]))
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

            if horz > size[0]:
                horz = size[0]
                N = True
                # north done

        elif not E:
            vert += random.randint(20, 200)

            if vert > size[1]:
                vert = size[1]
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


def gen_new():
    points = []
    r = random.randint(3, 100)
    p1 = (0, 0)
    p2 = (0, r)
    p3 = (r, 0)

    height = r

    while height < size[1]:
        while p1[0] < size[0]:
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


def gen_2():
    points = []
    x = random.randint(0, 100)
    p1 = (0, 0)
    p2 = (0, x)
    p3 = (x, 0)
    p4 = (random.randint(0, 100), random.randint(0, 100))

    height = x

    while height < size[1] + x:
        while p1[0] < size[0]:
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

def gen_3():
    points = []
    x = random.randint(0, 100)
    p1 = (0, 0)
    p2 = (0, x)
    p3 = (x, 0)
    p4 = (random.randint(0, 100), random.randint(0, 100))

    height = x

    while height < size[1] + x:
        while p1[0] < size[0]:
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
