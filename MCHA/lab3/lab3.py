#!/usr/bin/python2.7

import sympy
from sympy import *

def bisection(xValue, interval):
    numberOfIterations = 0
    left_d = interval[0]
    right_d = xValue[0]
    middle = (left_d + right_d) / 2
    while (abs(left_d - middle) > e) or (abs(right_d - middle) > e):
        if f[0].subs(x, left_d) * f[0].subs(x, middle) < 0:
            right_d = middle
        else:
            left_d = middle

        middle = (left_d + right_d) / 2
        numberOfIterations += 1

    print 'Bisection method: {}'.format(middle)
    print 'Number of iterations in the bisection method: {}'.format(numberOfIterations)



def chords(xValue, interval):
    two_diff = diff(f[1])
    numberOfItersInChords = 0
    if f[0].subs(x, xValue[0]) * two_diff.subs(x, xValue[0]) > 0:
        xh0 = left
        xh = xh0 - f[0].subs(x, xh0)*(xValue[0] - xh0)/(f[0].subs(x, xValue[0]) - f[0].subs(x, xh0))
        while (abs(xh - xh0) > e):
            xh0 = xh
            xh = xh0 - f[0].subs(x, xh0)*(xValue[0] - xh0) / (f[0].subs(x, xValue[0]) - f[0].subs(x, xh0))
            numberOfItersInChords += 1

    else:
        xh0 = xValue[0]
        xh = xh0 - f[0].subs(x, xh0)*(left - xh0)/(f[0].subs(x, left) - f[0].subs(x, xh0))
        while (abs(xh - xh0) > e):
            xh0 = xh
            xh = xh0 - f[0].subs(x, xh0) * (left - xh0) / (f[0].subs(x, left) - f[0].subs(x, xh0))
            numberOfItersInChords += 1

    print("Chords metod. Result:")
    print xh
    print 'Number of iterations in the method chord:'.format(numberIfItersInChords)


def main():
    n = 4
    a = -14.4621
    b = 60.6959
    c = -70.9238

    interval = (-10, 10)
    e = 0.0001
    x = sympy.Symbol("x")

    f = [0] * n

    f[0] = x**3 + a*x**2 + b*x + c
    f[1] = 3*x**2 + 2*a*x + b

    for i in xrange(2, n):
        f[i] = -sympy.prem(f[i - 2], f[i - 1])

    Na = 0
    Nb = 0
    for i in xrange(n - 1):
        polim = f[i]*f[i + 1]
        if polim.subs(x, interval[0]) < 0:
            Na += 1

        if polim.subs(x, interval[1]) < 0:
            Nb += 1

    print 'Number of roots: '.format(Na - Nb)

    xValue = solve(Eq(f[1], 0), x)
    print 'Root branch: '.format(xValue)

    bisection(xValue, interval)
    chords(xValue, interval)

    if __name__ == "__main__":
    main()
