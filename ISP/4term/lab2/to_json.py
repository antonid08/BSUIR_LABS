import json


class NotJsonSerializibleException(TypeError):
    def __init__(self, obj):
        self.obj = obj

    def __str__(self):
        return 'Type is not JSON serializible: ' + type(self.obj)


def transformString(string):
    return '"%s"' % string


def transformNumber(number):
    return str(number)


def transformBool(boolean):
    return str(boolean).lower()


def transformList(obj):
    result = '['
    items = []
    for item in obj:
        items.append(toJson(item))

    result += ', '.join(str(item) for item in items) + ']'
    return result


def transformDict(dictionary):
    result = '{'
    keys = dictionary.keys()
    pairs = []

    for key in keys:
        pairs.append(transformString(key) + ': ' + toJson(dictionary[key]))

    result += ', '.join(pair for pair in pairs) + '}'
    return result


def toJson(obj, raiseUnknown=False):
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


class Kek(object):
    k = 1000


def main():
    kek = [1, 2, 3]
    topkek = {'s': 1, 'k': 2}
    k = [kek, topkek]
    ka = Kek()
    print json.dumps(k)
    print toJson(ka)
    # print str(kek)
    # print json.dumps(kek)


if __name__ == '__main__':
    main()
