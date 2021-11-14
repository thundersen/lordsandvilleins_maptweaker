#!/bin/bash

NET_VERSION=`grep -oPm1 "(?<=<TargetFramework>)[^<]+" WorldTweaker.csproj`
PROJECT_DIR=`dirname $0`/../..
DLL_DIR=${PROJECT_DIR}/bin/Release/${NET_VERSION}
ZIP_FILE=`dirname $0`/worldtweaker_github.zip

rm -f ${ZIP_FILE}

zip -v -j ${ZIP_FILE} ${DLL_DIR}/WorldTweaker.dll 

echo "\n--- packaged github release at ${ZIP_FILE} ---\n"