#!/bin/bash

NET_VERSION=`grep -oPm1 "(?<=<TargetFramework>)[^<]+" WorldTweaker.csproj`
PROJECT_DIR=`dirname $0`/..
DLL_DIR=${PROJECT_DIR}/bin/Debug/${NET_VERSION}
TARGET_DIR=${PROJECT_DIR}/LordsAndVilleins/BepInEx/plugins/worldtweaker

mkdir -p ${TARGET_DIR}

cp -v ${DLL_DIR}/WorldTweaker.dll ${TARGET_DIR}