class Sequence(object):
    def __init__(self, iterable):
        if hasattr(iterable, '__iter__'):
            self.sequence = iterable
        else:
            raise TypeError('Iterable object expected.')
        self.counter = 0

    def __iter__(self):
        return self

    def next(self):
        if (self.counter < len(self.sequence)):
            obj = self.sequence[self.counter]
            self.counter += 1
            return obj

        self.counter = 0
        raise StopIteration

    def filter(self, function):
        return [item for item in self.sequence if function(item)]


def testFunc(x):
    return x > 5


def main():
    seq = Sequence(range(10))
    print seq.sequence
    print seq.filter(lambda x: x % 2)
    print seq.filter(testFunc)

if __name__ == '__main__':
    main()
