def fibGen(max):
    current = 1
    previous = 0
    try:
        for counter in xrange(max):
            temp = current + previous
            previous = current
            current = temp
            yield temp
    except OverflowError:
        print 'Sorry, very big number'

n = input()
for number in fibGen(n):
    print number
