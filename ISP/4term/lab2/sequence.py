class Sequence(object):
    def __init__(self, iterable):
        if hasattr(iterable, '__iter__'):
            self.sequence = iterable
        else:
            raise TypeError('Iterable object expected.')
        self.counter = 0

    def __iter__(self):
        return self.sequence.__iter__()

    def next(self):
        try:
            self.sequence[self.counter]
        except IndexError:
            self.counter = 0
            raise StopIteration
        finally:
            self.counter += 1

    def filter(self, function):
        offset = 0
        for i in range(len(self.sequence)):
            item = self.sequence[i - offset]
            if not function(item):
                self.sequence.remove(item)
                offset += 1

        return self.sequence


def testFunc(x):
    return x > 5


def main():
    seq = Sequence(range(10))
    print seq.sequence

    for i in seq:
        print i

    print seq.filter(testFunc)

    for i in seq:
        print i
    # print seq.filter(lambda x: x % 2)

if __name__ == '__main__':
    main()
