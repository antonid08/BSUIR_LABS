import re
import argparse


def getWordsFromText(text):
    """
    Get words from text.

        Args: Any string

        Return: List of words from this string

    """

    text = text.replace('.', ' ')
    text = text.replace('?', ' ')
    text = text.replace('!', ' ')
    text = text.replace(',', ' ')
    listOfWords = text.split()

    return listOfWords


def getSentencesFromText(text):
    """
    Split text to sentences.
        Args: Any string
        Return: List of sentences
    """

    sentences = re.split('\.|!|\?\\n', text)

    for sentence in sentences:
        if not sentence:
            sentences.remove(sentence)

    return sentences


def sortDict(unsortedDict):
    """
    Sort the dictionary
        Args: Any dictionary
        Return: Sorted list of dictionary's values
    """

    items = unsortedDict.items()
    items.sort(key=lambda item: item[1], reverse=True)

    return items


def getWordsDict(args):
    """
    Get dictionary with key=word, value=how many words like this in the text
        Args: commandline args with file, -p inside
        Return: Dictionary. key=word, value= haw many words like this in the text
    """

    listOfWords = getWordsFromText(args.f.read().lower())
    dictOfWords = {}
    for word in listOfWords:
        if dictOfWords.has_key(word):
            dictOfWords[word] += 1
        else:
            dictOfWords.update({word: 1})
    if args.p:
        print dictOfWords
    return dictOfWords


def getAverageNumberOfWordsInTheSentence(args):
    """
    Get avetage number of words in the sentence.
        Args: commandline args with file, --print inside
        Return: average number of woeds in the sentence
    """

    numberOfSentences = 0
    average = 0.0

    text = args.f.read()
    numberOfWords = len(getWordsFromText(text))

    numberOfSentences = text.count('. ') + text.count('? ') + text.count('! ')

    if text[-1] == '.' or text[-1] == '!' or text[-1] == '?':
        numberOfSentences += 1

    try:
        average = float(numberOfWords) / float(numberOfSentences)
    except ZeroDivisionError:
        average = numberOfWords

    if args.p:
        print 'Average number of words in sentences - %f' % average

    return average


def getMedianNumberOfWordsInTheSentece(args):
    """
    Get median number of words in the sentence.
        Args: commandline args with file, --print inside
        Return: median number of woeds in the sentence
    """

    text = args.f.read()
    sentences = getSentencesFromText(text)
    wordsInSentences = []

    for sentence in sentences:
        wordsInSentences.append(len(getWordsFromText(sentence)))

    wordsInSentences.sort()

    median = wordsInSentences[len(wordsInSentences) / 2]
    if args.p:
        print 'Median number of words in sentences - %f' % median

    return median


def getTopKNgrams(args):
    """
    Get Ton-K N-grams from the text.
        Args: commandline args with file, k, n, --print inside
        Return: Dictionary with key=ngramm, value=times
    """

    text = args.f.read()
    n = args.n
    k = args.k

    words = getWordsFromText(text)
    ngramms = {}
    for word in words:
        endNgramm = n

        wordLen = len(word)
        while endNgramm <= wordLen:
            ngramm = word[endNgramm - n:endNgramm]
            if ngramms.has_key(ngramm):
                ngramms[ngramm] += 1
            else:
                ngramms[ngramm] = 1
            endNgramm += 1

    sortedNgramms = sortDict(ngramms)

    if args.p:
        for counter in xrange(k):
            print sortedNgramms[counter][0] + ' - %d' % sortedNgramms[counter][1]
            counter += 1

    return sortedNgramms


def parse_args():
    """
    Parse commandline args.
        Return: args.
    """
    parser = argparse.ArgumentParser()
    subparsers = parser.add_subparsers()

    countWordsParser = subparsers.add_parser('count', help='Count all words')
    countWordsParser.set_defaults(func=getWordsDict)

    averageWordsParser = subparsers.add_parser('average', help='Count average\
                                                number of wors in sentence')
    averageWordsParser.set_defaults(func=getAverageNumberOfWordsInTheSentence)

    medianWordsParser = subparsers.add_parser('median', help='Count media\
                                              number of words in\
                                              sentence')
    medianWordsParser.set_defaults(func=getMedianNumberOfWordsInTheSentece)

    ngramsParser = subparsers.add_parser('ngrams', help='Get Top-K N-grams')
    ngramsParser.add_argument('k', type=int, help='Size of the top')
    ngramsParser.add_argument('n', type=int, help='Size of every N-gram')
    ngramsParser.set_defaults(func=getTopKNgrams)

    parser.add_argument('f', type=argparse.FileType('r'), help='Source file')
    parser.add_argument('-p', action='store_true', help='Print or no')

    return parser.parse_args()


args = parse_args()
args.func(args)
