import json


class NotJsonSerializibleException(TypeError):
    def __init__(self, obj):
        self.obj = obj

    def __str__(self):
        return 'Type is not JSON serializible: ' + type(self.obj)


def transformString(string):
    return '"%s"' % string.replace('\"', '\\\"')


def transformNumber(number):
    return str(number)


def transformBool(boolean):
    return str(boolean).lower()


def transformList(obj):
    result = '['
    items = []
    for item in obj:
        items.append(to_json(item))

    result += ', '.join(str(item) for item in items) + ']'
    return result


def transformDict(dictionary):
    result = '{'
    keys = dictionary.keys()
    pairs = []

    for key in keys:
        pairs.append(transformString(key) + ': ' + to_json(dictionary[key]))

    result += ', '.join(pair for pair in pairs) + '}'
    return result


def to_json(obj, raiseUnknown=False):
    if isinstance(obj, str):
        return transformString(obj)
    elif isinstance(obj, (int, float)):
        return transformNumber(obj)
    elif isinstance(obj, bool):
        return transformBool(obj)
    elif isinstance(obj, (list, tuple)):
        return transformList(obj)
    elif isinstance(obj, dict):
        return transformDict(obj)

    raise NotJsonSerializibleException(obj)


class Test(object):
    k = 1000


def main():
    test = [1, 2, 3]
    test2 = {'s': None, 'k': 2}
    k = [test, test2]
    test3 = 'ss\"'
    print json.dumps(k)
    print to_json(test3)
    # print str(kek)
    # print json.dumps(kek)


if __name__ == '__main__':
    main()
