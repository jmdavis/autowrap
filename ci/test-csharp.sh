#!/bin/bash

set -euo pipefail

SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
"$SCRIPT_DIR"/../csharp/tests/test.sh
make -j`nproc` -C "$SCRIPT_DIR"/.. test_cs