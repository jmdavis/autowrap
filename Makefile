# Makefile to drive the tests

DUB_CONFIGURATION ?= python37

.PHONY: test test_simple test_simple_py test_simple_cs test_issues test_pyd test_numpy examples/simple/libsimple.so examples/issues/libissues.so examples/pyd/libpydtests.so examples/numpy/libnumpy.so

all: test
test: test_simple test_issues test_pyd test_numpy
test_simple: test_simple_py test_simple_cs

clean:
	@rm examples/*/*.so examples/simple/simple examples/simple/Simple.cs

test_simple_py: tests/test_simple.py examples/simple/simple.so
	PYTHONPATH=$(PWD)/examples/simple pytest -s -vv tests/test_simple.py

examples/simple/simple.so: examples/simple/libsimple.so
	@cp $^ $@

examples/simple/libsimple.x64.so: examples/simple/libsimple.so
	@cp $^ $@

examples/simple/libsimple.so: examples/simple/dub.sdl examples/simple/dub.selections.json
	@cd examples/simple && dub build -q -c $(DUB_CONFIGURATION)

examples/simple/dub.selections.json:
	@cd examples/simple && dub upgrade -q

examples/simple/Simple.cs: examples/simple/libsimple.so
	@cd examples/simple && dub run -q --config=emitCSharp

test_issues: tests/test_issues.py examples/issues/issues.so
	PYTHONPATH=$(PWD)/examples/issues pytest -s -vv tests/test_issues.py

examples/issues/issues.so: examples/issues/libissues.so
	@cp $^ $@

examples/issues/libissues.so: examples/issues/dub.sdl examples/issues/dub.selections.json
	@cd examples/issues && dub build -q -c $(DUB_CONFIGURATION)

examples/issues/dub.selections.json:
	@cd examples/issues && dub upgrade -q

test_pyd: tests/test_pyd.py examples/pyd/pyd.so
	PYTHONPATH=$(PWD)/examples/pyd pytest -s -vv tests/test_pyd.py

examples/pyd/pyd.so: examples/pyd/libpydtests.so
	@cp $^ $@

examples/pyd/libpydtests.so: examples/pyd/dub.sdl examples/pyd/dub.selections.json
	@cd examples/pyd && dub build -q -c $(DUB_CONFIGURATION)

examples/pyd/dub.selections.json:
	@cd examples/pyd && dub upgrade -q

test_numpy: tests/test_numpy.py examples/numpy/numpytests.so
	PYTHONPATH=$(PWD)/examples/numpy pytest -s -vv tests/test_numpy.py

examples/numpy/numpytests.so: examples/numpy/libnumpy.so
	@cp $^ $@

examples/numpy/libnumpy.so: examples/numpy/dub.sdl examples/numpy/dub.selections.json
	@cd examples/numpy && dub build -q -c $(DUB_CONFIGURATION)

examples/numpy/dub.selections.json:
	@cd examples/numpy && dub upgrade -q

test_simple_cs: examples/simple/libsimple.x64.so examples/simple/Simple.cs
	@cd tests/test_simple_cs && \
		dotnet build test_simple_cs.csproj && \
		dotnet test test_simple_cs.csproj
