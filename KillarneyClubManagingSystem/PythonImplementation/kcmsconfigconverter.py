import json


class KCMSConfig:
    class IO:
        class Input:
            @classmethod
            def toDict(cls, kcmsconfig):
                r = {}
                currentBlock = None
                for x in kcmsconfig.split('\n'):
                    if (x[:1] != '#' or x[:2] == '##') and x != '':
                        # Non-comment
                        if x[:2] == '$ ':
                            print('setblock', x[2:])
                            currentBlock = x[2:]
                            continue
                        if currentBlock != None:
                            if currentBlock in r and isinstance(r[currentBlock], list):
                                r[currentBlock].append(x)
                            else:
                                r[currentBlock] = [x]
                return cls.resolveBlockNames(r)

            @classmethod
            def resolveBlockNames(cls, e):
                r = {}
                for x in e:
                    root = x.split(':')
                    d = r
                    for y in range(len(root)-1):
                        if root[y] in d:
                            d = d[root[y]]
                        else:
                            d[root[y]] = {}
                            d = d[root[y]]
                    d[root[-1]] = e[x]
                return r

        class Output:
            @classmethod
            def allElementsToString(cls, l):
                r = []
                # if not isinstance(l, list):
                #     raise TypeError('Use a list.')
                if isinstance(l, list):
                    for x in range(len(l)):
                        r.append(str(l[x]))
                    return r
                elif isinstance(l, KCMSConfig.MappedDataCompatiable):
                    return l.ConvertBack()

            @classmethod
            def toConfig(cls, KCMSConvertedDict):
                # pt = json.loads(plaintext)
                nodes = cls.getAllNodes(KCMSConvertedDict)
                out = ''
                for x in nodes:
                    out += '\n$ ' + x[0] + '\n' + \
                        '\n'.join(cls.allElementsToString(x[1]))
                return out

            @classmethod
            def getAllNodes(cls, dic, root=''):
                r = []
                if not isinstance(dic, dict):
                    return [(root, dic)]

                for x in dic:
                    rt = root + ':' + x if root != '' else x
                    r.extend(cls.getAllNodes(dic[x], rt))

                return r

        class JSON:
            @staticmethod
            def toJson(config):
                return json.dumps(KCMSConfig.IO.Input.toDict(config), indent=4, sort_keys=True)

            @staticmethod
            def fromJson(config):
                return KCMSConfig.IO.Output.toConfig(json.loads(config))

    class MappedDataCompatiable(object):
        def __init__(self, MappedDataRawList, sep='|'):
            super().__setattr__('MappedDataConverted', self._parseMDL_(MappedDataRawList, sep))
            super().__setattr__('sep', sep)

        def _parseMDL_(self, MDL, sep):
            r = {}
            for x in MDL:
                i = x.split(sep)
                if len(i) == 2:
                    r[i[0]] = i[1]
            return r

        def __setattr__(self, name, value):
            self.MappedDataConverted[name] = value
            return None

        def __getattr__(self, name):
            try:
                return super().__getattribute__(name)
            except:
                return self.MappedDataConverted[name]

        def __dict__(self):
            return self.MappedDataConverted

        def ConvertBack(self):
            r = []
            for x in self.MappedDataConverted:
                r.append(self.sep.join([x, str(self.MappedDataConverted[x])]))
            return r

        def __contains__(self, name):
            return name in self.MappedDataConverted

        def __str__(self):
            return str(self.MappedDataConverted)

        def __getitem__(self, key):
            return getattr(self, key)

        def __setitem__(self, key, value):
            return setattr(self, key, value)

        def __iter__(self):
            return iter(self.MappedDataConverted)


# a = KCMSConfig.MappedDataCompatiable([
#     '1|2',
#     'test|Hey',
#     'now|c'
# ])


# d = {
#     'b':{
#         'c':a
#     },
#     'c':{
#         'test':['1','2']
#     }
# }

# print(KCMSConfig.IO.Output.toConfig(d))
