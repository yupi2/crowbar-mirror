Imports System.IO
Imports System.Text

Public Class SourceSmdFile

#Region "Create and Destroy"

	Public Sub New()
		MyBase.New()

	End Sub

#End Region

#Region "Methods"

	Public Sub WriteReferenceAndLodSmdFiles(ByVal meshSmdPath As String, ByVal aSourceEngineModel As SourceModel)
		Me.theSourceEngineModel = aSourceEngineModel

		Dim smdPathFileName As String
		Dim aBodyPart As SourceVtxBodyPart
		Dim aVtxModel As SourceVtxModel
		Dim aModel As SourceMdlModel
		Dim bodyPartVertexIndexStart As Integer

		Me.theWeaponBoneIndex = -1

		If Me.theSourceEngineModel.theVtxFileHeader Is Nothing Then
			'TODO: Process animation-only mdl file.
			Exit Sub
		End If

		bodyPartVertexIndexStart = 0
		If Me.theSourceEngineModel.theVtxFileHeader.theVtxBodyParts IsNot Nothing AndAlso Me.theSourceEngineModel.theMdlFileHeader.theBodyParts IsNot Nothing Then
			For bodyPartIndex As Integer = 0 To Me.theSourceEngineModel.theVtxFileHeader.theVtxBodyParts.Count - 1
				aBodyPart = Me.theSourceEngineModel.theVtxFileHeader.theVtxBodyParts(bodyPartIndex)

				If aBodyPart.theVtxModels IsNot Nothing Then
					For modelIndex As Integer = 0 To aBodyPart.theVtxModels.Count - 1
						aVtxModel = aBodyPart.theVtxModels(modelIndex)

						If aVtxModel.theVtxModelLods IsNot Nothing Then
							aModel = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex)
							If aModel.name(0) = ChrW(0) Then
								Continue For
							End If

							For lodIndex As Integer = 0 To Me.theSourceEngineModel.theVtxFileHeader.lodCount - 1
								'TODO: Why would this count be different than the file header count?
								If lodIndex >= aVtxModel.theVtxModelLods.Count Then
									Exit For
								End If

								If lodIndex = 0 Then
									If Not TheApp.Settings.DecompileReferenceMeshSmdFileIsChecked Then
										Continue For
									End If
								ElseIf lodIndex > 0 Then
									If Not TheApp.Settings.DecompileLodMeshSmdFilesIsChecked Then
										Exit For
									End If
								End If

								smdPathFileName = Path.Combine(meshSmdPath, Me.theSourceEngineModel.GetBodyGroupSmdFileName(bodyPartIndex, modelIndex, lodIndex))

								Me.theOutputFileStream = File.CreateText(smdPathFileName)
								TheApp.SmdFilesWritten.Add(smdPathFileName)

								Me.WriteHeaderComment()

								Me.WriteHeaderSection()
								Me.WriteNodesSection(lodIndex)
								Me.WriteSkeletonSection(lodIndex)
								Me.WriteTrianglesSection(aVtxModel, lodIndex, aModel, bodyPartVertexIndexStart)

								Me.theOutputFileStream.Flush()
								Me.theOutputFileStream.Close()
							Next
							bodyPartVertexIndexStart += aModel.vertexCount
						End If
					Next
				End If
			Next
		End If
	End Sub

	Public Sub WriteCollisionSmdFile(ByVal pathFileName As String, ByVal aSourceEngineModel As SourceModel)
		Me.theSourceEngineModel = aSourceEngineModel
		If Me.theSourceEngineModel.thePhyFileHeader Is Nothing OrElse Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyCollisionDatas Is Nothing Then
			Exit Sub
		End If

		Me.theOutputFileStream = File.CreateText(pathFileName)
		TheApp.SmdFilesWritten.Add(pathFileName)

		Me.WriteHeaderComment()

		Me.WriteHeaderSection()
		Me.WriteNodesSection(-1)
		Me.WriteSkeletonSection(-1)
		Me.WriteTrianglesSectionForCollision()

		Me.theOutputFileStream.Flush()
		Me.theOutputFileStream.Close()
	End Sub

	Public Sub WriteAnimationSmdFile(ByVal pathFileName As String, ByVal aSourceEngineModel As SourceModel, ByVal aSequenceDesc As SourceMdlSequenceDesc, ByVal anAnimationDesc As SourceMdlAnimationDesc)
		Me.theSourceEngineModel = aSourceEngineModel

		'NOTE: File could already exist if the reference smd is also used for the first $sequence.
		' Could also exist if the loop through SequenceDescs has already created them before the loop through AnimationDescs
		' Store the names of every file saved up to now, and exit sub if in list.
		If TheApp.SmdFilesWritten.Contains(pathFileName) Then
			Exit Sub
		End If
		TheApp.SmdFilesWritten.Add(pathFileName)

		Me.theOutputFileStream = File.CreateText(pathFileName)

		Me.WriteHeaderComment()

		Me.WriteHeaderSection()
		Me.WriteNodesSection(-1)
		Me.WriteSkeletonSectionForAnimation(aSequenceDesc, anAnimationDesc)

		Me.theOutputFileStream.Flush()
		Me.theOutputFileStream.Close()
	End Sub

	Public Sub WriteVertexAnimationSmdFile(ByVal pathFileName As String, ByVal aSourceEngineModel As SourceModel)
		Me.theSourceEngineModel = aSourceEngineModel

		Me.theOutputFileStream = File.CreateText(pathFileName)

		Me.WriteHeaderComment()

		Me.WriteHeaderSection()
		Me.WriteNodesSection(-1)
		Me.WriteSkeletonSectionForVertexAnimation()
		Me.WriteVertexAnimationSection()

		Me.theOutputFileStream.Flush()
		Me.theOutputFileStream.Close()
	End Sub

#End Region

#Region "Private Methods"

	Private Sub WriteHeaderComment()
		Dim line As String = ""

		line = "// "
		line += TheApp.GetHeaderComment()
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteHeaderSection()
		Dim line As String = ""

		'version 1
		line = "version 1"
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteNodesSection(ByVal lodIndex As Integer)
		Dim line As String = ""
		Dim name As String

		'nodes
		line = "nodes"
		theOutputFileStream.WriteLine(line)

		For boneIndex As Integer = 0 To Me.theSourceEngineModel.theMdlFileHeader.theBones.Count - 1
			name = Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).theName
			If TheApp.Settings.DecompileApplyRightHandFixIsChecked AndAlso lodIndex = 0 AndAlso name = "ValveBiped.weapon_bone" Then
				Me.theWeaponBoneIndex = boneIndex
			End If

			line = "  "
			line += boneIndex.ToString(TheApp.InternalNumberFormat)
			line += " """
			line += name
			line += """ "
			line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex.ToString(TheApp.InternalNumberFormat)
			theOutputFileStream.WriteLine(line)
		Next

		line = "end"
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteSkeletonSection(ByVal lodIndex As Integer)
		Dim line As String = ""

		'skeleton
		line = "skeleton"
		theOutputFileStream.WriteLine(line)

		line = "  time 0"
		theOutputFileStream.WriteLine(line)
		For boneIndex As Integer = 0 To Me.theSourceEngineModel.theMdlFileHeader.theBones.Count - 1
			line = "    "
			line += boneIndex.ToString(TheApp.InternalNumberFormat)
			line += " "
			If lodIndex = 0 AndAlso Me.theWeaponBoneIndex = boneIndex Then
				line += "0.000000 0.000000 0.000000"
				'ElseIf Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
				'	'NOTE: Only adjust position if a root bone. Did not seem to help for l4d2's van.mdl.
				'	line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).positionY.ToString("0.000000", TheApp.InternalNumberFormat)
				'	line += " "
				'	line += (-Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).positionX).ToString("0.000000", TheApp.InternalNumberFormat)
				'	line += " "
				'	line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).positionZ.ToString("0.000000", TheApp.InternalNumberFormat)
			Else
				line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).position.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).position.y.ToString("0.000000", TheApp.InternalNumberFormat)
				'line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).positionY.ToString("0.000000", TheApp.InternalNumberFormat)
				'line += " "
				'line += (-Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).positionX).ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).position.z.ToString("0.000000", TheApp.InternalNumberFormat)
			End If
			line += " "
			line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).rotation.x.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).rotation.y.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).rotation.z.ToString("0.000000", TheApp.InternalNumberFormat)
			theOutputFileStream.WriteLine(line)
		Next

		line = "end"
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteSkeletonSectionForAnimation(ByVal aSequenceDesc As SourceMdlSequenceDesc, ByVal anAnimationDesc As SourceMdlAnimationDesc)
		Dim line As String = ""
		Dim boneIndex As Integer
		Dim aFrameLine As AnimationFrameLine

		'skeleton
		line = "skeleton"
		theOutputFileStream.WriteLine(line)

		Me.theAnimationFrameLines = New SortedList(Of Integer, AnimationFrameLine)()
		''NOTE: MDL Decompiler uses 0 to frameCount, which is not what I would expect.
		'For frameIndex As Integer = 0 To anAnimationDesc.frameCount
		For frameIndex As Integer = 0 To anAnimationDesc.frameCount - 1
			Me.theAnimationFrameLines.Clear()
			If (anAnimationDesc.flags And SourceMdlAnimationDesc.STUDIO_ALLZEROS) = 0 Then
				Me.CalcAnimation(aSequenceDesc, anAnimationDesc, frameIndex)
			End If

			line = "  time "
			line += CStr(frameIndex)
			theOutputFileStream.WriteLine(line)

			For i As Integer = 0 To Me.theAnimationFrameLines.Count - 1
				boneIndex = Me.theAnimationFrameLines.Keys(i)
				aFrameLine = Me.theAnimationFrameLines.Values(i)

				line = "    "
				line += boneIndex.ToString(TheApp.InternalNumberFormat)

				line += " "
				'NOTE: Only adjust position if bone is a root bone. Do not know why.
				If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
					line += aFrameLine.position.y.ToString("0.000000", TheApp.InternalNumberFormat)
					line += " "
					line += (-aFrameLine.position.x).ToString("0.000000", TheApp.InternalNumberFormat)
				Else
					line += aFrameLine.position.x.ToString("0.000000", TheApp.InternalNumberFormat)
					line += " "
					line += aFrameLine.position.y.ToString("0.000000", TheApp.InternalNumberFormat)
				End If
				line += " "
				line += aFrameLine.position.z.ToString("0.000000", TheApp.InternalNumberFormat)

				line += " "
				line += aFrameLine.rotation.x.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				''TEST: Only rotate by 90 deg if a root bone.
				'If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
				'	Dim y As Double
				'	y = aFrameLine.rotation.y
				'	' Rotate y by -90 degrees.
				'	y += MathModule.DegreesToRadians(-90)
				'	line += y.ToString("0.000000", TheApp.InternalNumberFormat)
				'Else
				line += aFrameLine.rotation.y.ToString("0.000000", TheApp.InternalNumberFormat)
				'End If
				line += " "
				'NOTE: Only rotate by -90 deg if bone is a root bone.  Do not know why.
				If Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex).parentBoneIndex = -1 Then
					Dim z As Double
					z = aFrameLine.rotation.z
					z += MathModule.DegreesToRadians(-90)
					line += z.ToString("0.000000", TheApp.InternalNumberFormat)
				Else
					line += aFrameLine.rotation.z.ToString("0.000000", TheApp.InternalNumberFormat)
				End If

				If TheApp.Settings.DecompileBoneAnimationDebugInfoIsChecked Then
					line += "   # "
					line += "pos: "
					line += aFrameLine.position.debug_text
					line += "   "
					line += "rot: "
					line += aFrameLine.rotation.debug_text
				End If

				theOutputFileStream.WriteLine(line)
			Next
		Next

		line = "end"
		theOutputFileStream.WriteLine(line)
	End Sub

	'static void CalcAnimation( const CStudioHdr *pStudioHdr,	Vector *pos, Quaternion *q, 
	'	mstudioseqdesc_t &seqdesc,
	'	int sequence, int animation,
	'	float cycle, int boneMask )
	'{
	'	int					i;
	'
	'	mstudioanimdesc_t &animdesc = pStudioHdr->pAnimdesc( animation );
	'	mstudiobone_t *pbone = pStudioHdr->pBone( 0 );
	'	mstudioanim_t *panim = animdesc.pAnim( );
	'
	'	int					iFrame;
	'	float				s;
	'
	'	float fFrame = cycle * (animdesc.numframes - 1);
	'
	'	iFrame = (int)fFrame;
	'	s = (fFrame - iFrame);
	'
	'	float *pweight = seqdesc.pBoneweight( 0 );
	'
	'	for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
	'	{
	'		if (panim && panim->bone == i)
	'		{
	'			if (*pweight > 0 && (pbone->flags & boneMask))
	'			{
	'				CalcBoneQuaternion( iFrame, s, pbone, panim, q[i] );
	'				CalcBonePosition  ( iFrame, s, pbone, panim, pos[i] );
	'			}
	'			panim = panim->pNext();
	'		}
	'		else if (*pweight > 0 && (pbone->flags & boneMask))
	'		{
	'			if (animdesc.flags & STUDIO_DELTA)
	'			{
	'				q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'				pos[i].Init( 0.0f, 0.0f, 0.0f );
	'			}
	'			else
	'			{
	'				q[i] = pbone->quat;
	'				pos[i] = pbone->pos;
	'			}
	'		}
	'	}
	'}
	'======
	'FROM: SourceEngine2007_source\src_main\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: Find and decode a sub-frame of animation
	'//-----------------------------------------------------------------------------
	'
	'static void CalcAnimation( const CStudioHdr *pStudioHdr,	Vector *pos, Quaternion *q, 
	'	mstudioseqdesc_t &seqdesc,
	'	int sequence, int animation,
	'	float cycle, int boneMask )
	'{
	'#ifdef STUDIO_ENABLE_PERF_COUNTERS
	'	pStudioHdr->m_nPerfAnimationLayers++;
	'#endif
	'
	'	virtualmodel_t *pVModel = pStudioHdr->GetVirtualModel();
	'
	'	if (pVModel)
	'	{
	'		CalcVirtualAnimation( pVModel, pStudioHdr, pos, q, seqdesc, sequence, animation, cycle, boneMask );
	'		return;
	'	}
	'
	'	mstudioanimdesc_t &animdesc = pStudioHdr->pAnimdesc( animation );
	'	mstudiobone_t *pbone = pStudioHdr->pBone( 0 );
	'	const mstudiolinearbone_t *pLinearBones = pStudioHdr->pLinearBones();
	'
	'	int					i;
	'	int					iFrame;
	'	float				s;
	'
	'	float fFrame = cycle * (animdesc.numframes - 1);
	'
	'	iFrame = (int)fFrame;
	'	s = (fFrame - iFrame);
	'
	'	int iLocalFrame = iFrame;
	'	float flStall;
	'	mstudioanim_t *panim = animdesc.pAnim( &iLocalFrame, flStall );
	'
	'	float *pweight = seqdesc.pBoneweight( 0 );
	'
	'	// if the animation isn't available, look for the zero frame cache
	'	if (!panim)
	'	{
	'		// Msg("zeroframe %s\n", animdesc.pszName() );
	'		// pre initialize
	'		for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
	'		{
	'			if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
	'			{
	'				if (animdesc.flags & STUDIO_DELTA)
	'				{
	'					q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'					pos[i].Init( 0.0f, 0.0f, 0.0f );
	'				}
	'				else
	'				{
	'					q[i] = pbone->quat;
	'					pos[i] = pbone->pos;
	'				}
	'			}
	'		}
	'
	'		CalcZeroframeData( pStudioHdr, pStudioHdr->GetRenderHdr(), NULL, pStudioHdr->pBone( 0 ), animdesc, fFrame, pos, q, boneMask, 1.0 );
	'
	'		return;
	'	}
	'
	'	// BUGBUG: the sequence, the anim, and the model can have all different bone mappings.
	'	for (i = 0; i < pStudioHdr->numbones(); i++, pbone++, pweight++)
	'	{
	'		if (panim && panim->bone == i)
	'		{
	'			if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
	'			{
	'				CalcBoneQuaternion( iLocalFrame, s, pbone, pLinearBones, panim, q[i] );
	'				CalcBonePosition  ( iLocalFrame, s, pbone, pLinearBones, panim, pos[i] );
	'#ifdef STUDIO_ENABLE_PERF_COUNTERS
	'				pStudioHdr->m_nPerfAnimatedBones++;
	'				pStudioHdr->m_nPerfUsedBones++;
	'#endif
	'			}
	'			panim = panim->pNext();
	'		}
	'		else if (*pweight > 0 && (pStudioHdr->boneFlags(i) & boneMask))
	'		{
	'			if (animdesc.flags & STUDIO_DELTA)
	'			{
	'				q[i].Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'				pos[i].Init( 0.0f, 0.0f, 0.0f );
	'			}
	'			else
	'			{
	'				q[i] = pbone->quat;
	'				pos[i] = pbone->pos;
	'			}
	'#ifdef STUDIO_ENABLE_PERF_COUNTERS
	'			pStudioHdr->m_nPerfUsedBones++;
	'#endif
	'		}
	'	}
	'
	'	// cross fade in previous zeroframe data
	'	if (flStall > 0.0f)
	'	{
	'		CalcZeroframeData( pStudioHdr, pStudioHdr->GetRenderHdr(), NULL, pStudioHdr->pBone( 0 ), animdesc, fFrame, pos, q, boneMask, flStall );
	'	}
	'
	'	if (animdesc.numlocalhierarchy)
	'	{
	'		matrix3x4_t *boneToWorld = g_MatrixPool.Alloc();
	'		CBoneBitList boneComputed;
	'
	'		int i;
	'		for (i = 0; i < animdesc.numlocalhierarchy; i++)
	'		{
	'			mstudiolocalhierarchy_t *pHierarchy = animdesc.pHierarchy( i );
	'
	'			if ( !pHierarchy )
	'				break;
	'
	'			if (pStudioHdr->boneFlags(pHierarchy->iBone) & boneMask)
	'			{
	'				if (pStudioHdr->boneFlags(pHierarchy->iNewParent) & boneMask)
	'				{
	'					CalcLocalHierarchyAnimation( pStudioHdr, boneToWorld, boneComputed, pos, q, pbone, pHierarchy, pHierarchy->iBone, pHierarchy->iNewParent, cycle, iFrame, s, boneMask );
	'				}
	'			}
	'		}
	'
	'		g_MatrixPool.Free( boneToWorld );
	'	}
	'
	'}
	Private Sub CalcAnimation(ByVal aSequenceDesc As SourceMdlSequenceDesc, ByVal anAnimationDesc As SourceMdlAnimationDesc, ByVal frameIndex As Integer)
		Dim s As Double
		Dim animIndex As Integer
		Dim aBone As SourceMdlBone
		Dim aWeight As Double
		Dim anAnimation As SourceMdlAnimation
		Dim rot As SourceVector
		Dim pos As SourceVector
		Dim aFrameLine As AnimationFrameLine
		Dim sectionFrameIndex As Integer

		s = 0

		animIndex = 0

		'If anAnimationDesc.theAnimations Is Nothing OrElse animIndex >= anAnimationDesc.theAnimations.Count Then
		'	anAnimation = Nothing
		'Else
		'	anAnimation = anAnimationDesc.theAnimations(animIndex)
		'End If
		'------
		Dim sectionIndex As Integer
		Dim aSectionOfAnimation As List(Of SourceMdlAnimation)
		If anAnimationDesc.sectionFrameCount = 0 Then
			sectionIndex = 0
			sectionFrameIndex = frameIndex
		Else
			sectionIndex = CInt(Math.Truncate(frameIndex / anAnimationDesc.sectionFrameCount))
			sectionFrameIndex = frameIndex - (sectionIndex * anAnimationDesc.sectionFrameCount)
		End If
		aSectionOfAnimation = anAnimationDesc.theSectionsOfAnimations(sectionIndex)
		If anAnimationDesc.theSectionsOfAnimations Is Nothing OrElse animIndex >= aSectionOfAnimation.Count Then
			anAnimation = Nothing
		Else
			anAnimation = aSectionOfAnimation(animIndex)
		End If

		For boneIndex As Integer = 0 To Me.theSourceEngineModel.theMdlFileHeader.theBones.Count - 1
			aBone = Me.theSourceEngineModel.theMdlFileHeader.theBones(boneIndex)

			If aSequenceDesc IsNot Nothing Then
				aWeight = aSequenceDesc.theBoneWeights(boneIndex)
			Else
				'NOTE: This should only be needed for a delta $animation.
				'      Arbitrarily assign 1 so that the following code will add frame lines for this $animation.
				aWeight = 1
			End If

			If anAnimation IsNot Nothing AndAlso anAnimation.boneIndex = boneIndex Then
				If aWeight > 0 Then
					If Me.theAnimationFrameLines.ContainsKey(boneIndex) Then
						aFrameLine = Me.theAnimationFrameLines(boneIndex)
					Else
						aFrameLine = New AnimationFrameLine()
						Me.theAnimationFrameLines.Add(boneIndex, aFrameLine)
					End If

					'rot = CalcBoneRotation(frameIndex, s, aBone, anAnimation)
					rot = CalcBoneRotation(sectionFrameIndex, s, aBone, anAnimation)
					aFrameLine.rotation = New SourceVector()

					'NOTE: z = z puts head-foot axis horizontally
					'      facing viewer
					aFrameLine.rotation.x = rot.x
					aFrameLine.rotation.y = rot.y
					aFrameLine.rotation.z = rot.z
					'------
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = rot.y - 1.570796
					'aFrameLine.rotation.z = rot.z
					'------
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = rot.x
					'aFrameLine.rotation.z = rot.z
					'------
					'------
					'NOTE: x = z puts head-foot axis horizontally
					'      facing away from viewer
					'aFrameLine.rotation.x = rot.z
					'aFrameLine.rotation.y = rot.y
					'aFrameLine.rotation.z = rot.x
					'------
					'aFrameLine.rotation.x = rot.z
					'aFrameLine.rotation.y = rot.x
					'aFrameLine.rotation.z = rot.y
					'------
					'------
					'NOTE: y = z  : head-foot axis vertically correctly
					'      x = -x : upside-down
					'      z = y  : 
					' facing to window right
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = rot.y
					'------
					'NOTE: Upside-down; facing to window left
					'aFrameLine.rotation.x = -rot.x
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = rot.y
					'------
					' facing to window right
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = rot.y
					'------
					'NOTE: Upside-down; facing to window left
					'aFrameLine.rotation.x = -rot.x
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = rot.y
					'------
					' facing to window left
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = -rot.y
					'------
					'NOTE: Upside-down; facing to window right
					'aFrameLine.rotation.x = -rot.x
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = -rot.y
					'------
					' facing to window left
					'aFrameLine.rotation.x = rot.x
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = -rot.y
					'------
					'NOTE: Upside-down; facing to window right
					'aFrameLine.rotation.x = -rot.x
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = -rot.y
					'------
					'------
					' facing to window right
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = rot.x
					'------
					'NOTE: Upside-down; facing to window left
					'aFrameLine.rotation.x = -rot.y
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = rot.x
					'------
					' facing to window right
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = rot.x
					'------
					'aFrameLine.rotation.x = -rot.y
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = rot.x
					'------
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = -rot.x
					'------
					'aFrameLine.rotation.x = -rot.y
					'aFrameLine.rotation.y = rot.z
					'aFrameLine.rotation.z = -rot.x
					'------
					'aFrameLine.rotation.x = rot.y
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = -rot.x
					'------
					'aFrameLine.rotation.x = -rot.y
					'aFrameLine.rotation.y = -rot.z
					'aFrameLine.rotation.z = -rot.x

					aFrameLine.rotation.debug_text = rot.debug_text

					'pos = Me.CalcBonePosition(frameIndex, s, aBone, anAnimation)
					pos = Me.CalcBonePosition(sectionFrameIndex, s, aBone, anAnimation)
					aFrameLine.position = New SourceVector()
					aFrameLine.position.x = pos.x
					aFrameLine.position.y = pos.y
					aFrameLine.position.z = pos.z
					aFrameLine.position.debug_text = pos.debug_text
				End If

				animIndex += 1
				'If animIndex >= anAnimationDesc.theAnimations.Count Then
				'	anAnimation = Nothing
				'Else
				'	anAnimation = anAnimationDesc.theAnimations(animIndex)
				'End If
				If animIndex >= aSectionOfAnimation.Count Then
					anAnimation = Nothing
				Else
					anAnimation = aSectionOfAnimation(animIndex)
				End If
			ElseIf aWeight > 0 Then
				If Me.theAnimationFrameLines.ContainsKey(boneIndex) Then
					aFrameLine = Me.theAnimationFrameLines(boneIndex)
				Else
					aFrameLine = New AnimationFrameLine()
					Me.theAnimationFrameLines.Add(boneIndex, aFrameLine)
				End If

				If (anAnimationDesc.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
					aFrameLine.rotation = New SourceVector()
					aFrameLine.rotation.x = 0
					aFrameLine.rotation.y = 0
					aFrameLine.rotation.z = 0
					aFrameLine.rotation.debug_text = "desc_delta"

					aFrameLine.position = New SourceVector()
					aFrameLine.position.x = 0
					aFrameLine.position.y = 0
					aFrameLine.position.z = 0
					aFrameLine.position.debug_text = "desc_delta"
				Else
					aFrameLine.rotation = New SourceVector()
					aFrameLine.rotation.x = aBone.rotation.x
					aFrameLine.rotation.y = aBone.rotation.y
					aFrameLine.rotation.z = aBone.rotation.z
					aFrameLine.rotation.debug_text = "desc_bone"

					aFrameLine.position = New SourceVector()
					aFrameLine.position.x = aBone.position.x
					aFrameLine.position.y = aBone.position.y
					aFrameLine.position.z = aBone.position.z
					aFrameLine.position.debug_text = "desc_bone"
				End If
			End If
		Next
	End Sub

	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: return a sub frame rotation for a single bone
	'//-----------------------------------------------------------------------------
	'void CalcBoneQuaternion( int frame, float s, 
	'						const Quaternion &baseQuat, const RadianEuler &baseRot, const Vector &baseRotScale, 
	'						int iBaseFlags, const Quaternion &baseAlignment, 
	'						const mstudioanim_t *panim, Quaternion &q )
	'{
	'	if ( panim->flags & STUDIO_ANIM_RAWROT )
	'	{
	'		q = *(panim->pQuat48());
	'		Assert( q.IsValid() );
	'		return;
	'	} 

	'	if ( panim->flags & STUDIO_ANIM_RAWROT2 )
	'	{
	'		q = *(panim->pQuat64());
	'		Assert( q.IsValid() );
	'		return;
	'	}

	'	if ( !(panim->flags & STUDIO_ANIM_ANIMROT) )
	'	{
	'		if (panim->flags & STUDIO_ANIM_DELTA)
	'		{
	'			q.Init( 0.0f, 0.0f, 0.0f, 1.0f );
	'		}
	'		else
	'		{
	'			q = baseQuat;
	'		}
	'		return;
	'	}

	'	mstudioanim_valueptr_t *pValuesPtr = panim->pRotV();

	'	if (s > 0.001f)
	'	{
	'		QuaternionAligned	q1, q2;
	'		RadianEuler			angle1, angle2;

	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 0 ), baseRotScale.x, angle1.x, angle2.x );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 1 ), baseRotScale.y, angle1.y, angle2.y );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 2 ), baseRotScale.z, angle1.z, angle2.z );

	'		if (!(panim->flags & STUDIO_ANIM_DELTA))
	'		{
	'			angle1.x = angle1.x + baseRot.x;
	'			angle1.y = angle1.y + baseRot.y;
	'			angle1.z = angle1.z + baseRot.z;
	'			angle2.x = angle2.x + baseRot.x;
	'			angle2.y = angle2.y + baseRot.y;
	'			angle2.z = angle2.z + baseRot.z;
	'		}

	'		Assert( angle1.IsValid() && angle2.IsValid() );
	'		if (angle1.x != angle2.x || angle1.y != angle2.y || angle1.z != angle2.z)
	'		{
	'			AngleQuaternion( angle1, q1 );
	'			AngleQuaternion( angle2, q2 );

	'	#ifdef _X360
	'			fltx4 q1simd, q2simd, qsimd;
	'			q1simd = LoadAlignedSIMD( q1 );
	'			q2simd = LoadAlignedSIMD( q2 );
	'			qsimd = QuaternionBlendSIMD( q1simd, q2simd, s );
	'			StoreUnalignedSIMD( q.Base(), qsimd );
	'	#else
	'			QuaternionBlend( q1, q2, s, q );
	'#End If
	'		}
	'		else
	'		{
	'			AngleQuaternion( angle1, q );
	'		}
	'	}
	'	else
	'	{
	'		RadianEuler			angle;

	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 0 ), baseRotScale.x, angle.x );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 1 ), baseRotScale.y, angle.y );
	'		ExtractAnimValue( frame, pValuesPtr->pAnimvalue( 2 ), baseRotScale.z, angle.z );

	'		if (!(panim->flags & STUDIO_ANIM_DELTA))
	'		{
	'			angle.x = angle.x + baseRot.x;
	'			angle.y = angle.y + baseRot.y;
	'			angle.z = angle.z + baseRot.z;
	'		}

	'		Assert( angle.IsValid() );
	'		AngleQuaternion( angle, q );
	'	}

	'	Assert( q.IsValid() );

	'	// align to unified bone
	'	if (!(panim->flags & STUDIO_ANIM_DELTA) && (iBaseFlags & BONE_FIXED_ALIGNMENT))
	'	{
	'		QuaternionAlign( baseAlignment, q, q );
	'	}
	'}
	'
	'inline void CalcBoneQuaternion( int frame, float s, 
	'						const mstudiobone_t *pBone,
	'						const mstudiolinearbone_t *pLinearBones,
	'						const mstudioanim_t *panim, Quaternion &q )
	'{
	'	if (pLinearBones)
	'	{
	'		CalcBoneQuaternion( frame, s, pLinearBones->quat(panim->bone), pLinearBones->rot(panim->bone), pLinearBones->rotscale(panim->bone), pLinearBones->flags(panim->bone), pLinearBones->qalignment(panim->bone), panim, q );
	'	}
	'	else
	'	{
	'		CalcBoneQuaternion( frame, s, pBone->quat, pBone->rot, pBone->rotscale, pBone->flags, pBone->qAlignment, panim, q );
	'	}
	'}
	'Private Function CalcBoneQuaternion(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone, ByVal anAnimation As SourceMdlAnimation) As SourceQuaternion
	'	Dim rot As New SourceQuaternion()
	'	Dim angleVector As New SourceVector()

	'	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
	'		rot.x = anAnimation.theRot48.x
	'		rot.y = anAnimation.theRot48.y
	'		rot.z = anAnimation.theRot48.z
	'		rot.w = anAnimation.theRot48.w
	'		Return rot
	'	ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
	'		rot.x = anAnimation.theRot64.x
	'		rot.y = anAnimation.theRot64.y
	'		rot.z = anAnimation.theRot64.z
	'		rot.w = anAnimation.theRot64.w
	'		Return rot
	'	End If

	'	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) = 0 Then
	'		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
	'			rot.x = 0
	'			rot.y = 0
	'			rot.z = 0
	'			rot.w = 1
	'		Else
	'			rot.x = aBone.quat.x
	'			rot.y = aBone.quat.y
	'			rot.z = aBone.quat.z
	'			rot.w = aBone.quat.w
	'		End If
	'		Return rot
	'	End If

	'	Dim rotV As SourceMdlAnimationValuePointer

	'	rotV = anAnimation.theRotV

	'	If rotV.animValueOffset(0) <= 0 Then
	'		angleVector.x = 0
	'	Else
	'		angleVector.x = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(0), aBone.rotationScaleX)
	'	End If
	'	If rotV.animValueOffset(1) <= 0 Then
	'		angleVector.y = 0
	'	Else
	'		angleVector.y = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(1), aBone.rotationScaleY)
	'	End If
	'	If rotV.animValueOffset(2) <= 0 Then
	'		angleVector.z = 0
	'	Else
	'		angleVector.z = Me.ExtractAnimValue(frameIndex, rotV.theAnimValues(2), aBone.rotationScaleZ)
	'	End If

	'	If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) = 0 Then
	'		angleVector.x += aBone.quat.x
	'		angleVector.y += aBone.quat.y
	'		angleVector.z += aBone.quat.z
	'	End If

	'	rot = MathModule.AngleQuaternion(angleVector)

	'	'	if (!(panim->flags & STUDIO_ANIM_DELTA) && (iBaseFlags & BONE_FIXED_ALIGNMENT))
	'	'	{
	'	'		QuaternionAlign( baseAlignment, q, q );
	'	'	}

	'	Return rot
	'End Function

	Private Function CalcBoneRotation(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone, ByVal anAnimation As SourceMdlAnimation) As SourceVector
		Dim rot As New SourceQuaternion()
		Dim angleVector As New SourceVector()

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT) > 0 Then
			rot.x = anAnimation.theRot48.x
			rot.y = anAnimation.theRot48.y
			rot.z = anAnimation.theRot48.z
			rot.w = anAnimation.theRot48.w
			angleVector = MathModule.ToEulerAngles(rot)

			angleVector.debug_text = "raw48"
			Return angleVector
		ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWROT2) > 0 Then
			'angleVector.x = anAnimation.theRot64.xRadians
			'angleVector.y = anAnimation.theRot64.yRadians
			'angleVector.z = anAnimation.theRot64.zRadians
			'------
			rot.x = anAnimation.theRot64.x
			rot.y = anAnimation.theRot64.y
			rot.z = anAnimation.theRot64.z
			rot.w = anAnimation.theRot64.w
			angleVector = MathModule.ToEulerAngles(rot)

			''TEST: Rotate z by -90 degrees.
			''TEST: Rotate y by -90 degrees.
			'angleVector.y += MathModule.DegreesToRadians(-90)

			angleVector.debug_text = "raw64 (" + rot.x.ToString() + ", " + rot.y.ToString() + ", " + rot.z.ToString() + ", " + rot.w.ToString() + ")"
			Return angleVector
		End If

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMROT) = 0 Then
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
				angleVector.x = 0
				angleVector.y = 0
				angleVector.z = 0
				angleVector.debug_text = "delta"
			Else
				angleVector.x = aBone.rotation.x
				angleVector.y = aBone.rotation.y
				angleVector.z = aBone.rotation.z
				angleVector.debug_text = "bone"
			End If
			Return angleVector
		End If

		Dim rotV As SourceMdlAnimationValuePointer

		rotV = anAnimation.theRotV

		If rotV.animXValueOffset <= 0 Then
			angleVector.x = 0
		Else
			angleVector.x = Me.ExtractAnimValue(frameIndex, rotV.theAnimXValues, aBone.rotationScale.x)
		End If
		If rotV.animYValueOffset <= 0 Then
			angleVector.y = 0
		Else
			angleVector.y = Me.ExtractAnimValue(frameIndex, rotV.theAnimYValues, aBone.rotationScale.y)
		End If
		If rotV.animZValueOffset <= 0 Then
			angleVector.z = 0
		Else
			angleVector.z = Me.ExtractAnimValue(frameIndex, rotV.theAnimZValues, aBone.rotationScale.z)
		End If

		angleVector.debug_text = "anim"

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) = 0 Then
			angleVector.x += aBone.rotation.x
			angleVector.y += aBone.rotation.y
			angleVector.z += aBone.rotation.z
			angleVector.debug_text += "+bone"
		End If

		Return angleVector
	End Function

	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'//-----------------------------------------------------------------------------
	'// Purpose: return a sub frame position for a single bone
	'//-----------------------------------------------------------------------------
	'void CalcBonePosition(	int frame, float s,
	'						const Vector &basePos, const Vector &baseBoneScale, 
	'						const mstudioanim_t *panim, Vector &pos	)
	'{
	'	if (panim->flags & STUDIO_ANIM_RAWPOS)
	'	{
	'		pos = *(panim->pPos());
	'		Assert( pos.IsValid() );

	'		return;
	'	}
	'	else if (!(panim->flags & STUDIO_ANIM_ANIMPOS))
	'	{
	'		if (panim->flags & STUDIO_ANIM_DELTA)
	'		{
	'			pos.Init( 0.0f, 0.0f, 0.0f );
	'		}
	'		else
	'		{
	'			pos = basePos;
	'		}
	'		return;
	'	}

	'	mstudioanim_valueptr_t *pPosV = panim->pPosV();
	'	int					j;

	'	if (s > 0.001f)
	'	{
	'		float v1, v2;
	'		for (j = 0; j < 3; j++)
	'		{
	'			ExtractAnimValue( frame, pPosV->pAnimvalue( j ), baseBoneScale[j], v1, v2 );
	'			//ZM: This is really setting pos.x when j = 0, pos.y when j = 1, and pos.z when j = 2.
	'			pos[j] = v1 * (1.0 - s) + v2 * s;
	'		}
	'	}
	'	else
	'	{
	'		for (j = 0; j < 3; j++)
	'		{
	'			//ZM: This is really setting pos.x when j = 0, pos.y when j = 1, and pos.z when j = 2.
	'			ExtractAnimValue( frame, pPosV->pAnimvalue( j ), baseBoneScale[j], pos[j] );
	'		}
	'	}

	'	if (!(panim->flags & STUDIO_ANIM_DELTA))
	'	{
	'		pos.x = pos.x + basePos.x;
	'		pos.y = pos.y + basePos.y;
	'		pos.z = pos.z + basePos.z;
	'	}

	'	Assert( pos.IsValid() );
	'}
	Private Function CalcBonePosition(ByVal frameIndex As Integer, ByVal s As Double, ByVal aBone As SourceMdlBone, ByVal anAnimation As SourceMdlAnimation) As SourceVector
		Dim pos As New SourceVector()

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_RAWPOS) > 0 Then
			'If aBone.parentBoneIndex = -1 Then
			'	pos.x = anAnimation.thePos.y
			'	pos.y = -anAnimation.thePos.x
			'	pos.z = anAnimation.thePos.z
			'Else
			pos.x = anAnimation.thePos.x
			pos.y = anAnimation.thePos.y
			pos.z = anAnimation.thePos.z
			'	'------
			'	'pos.y = anAnimation.thePos.z
			'	'pos.z = anAnimation.thePos.y
			'	'------
			'	'pos.x = anAnimation.thePos.y
			'	'pos.y = -anAnimation.thePos.x
			'	'pos.z = anAnimation.thePos.z
			'End If

			pos.debug_text = "raw"
			Return pos
		ElseIf (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_ANIMPOS) = 0 Then
			If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) > 0 Then
				pos.x = 0
				pos.y = 0
				pos.z = 0
				pos.debug_text = "delta"
			Else
				pos.x = aBone.position.x
				pos.y = aBone.position.y
				pos.z = aBone.position.z
				'pos.y = aBone.positionZ
				'pos.z = -aBone.positionY
				pos.debug_text = "bone"
			End If
			Return pos
		End If

		Dim posV As SourceMdlAnimationValuePointer

		posV = anAnimation.thePosV

		If posV.animXValueOffset <= 0 Then
			pos.x = 0
		Else
			pos.x = Me.ExtractAnimValue(frameIndex, posV.theAnimXValues, aBone.positionScale.x)
		End If

		If posV.animYValueOffset <= 0 Then
			pos.y = 0
		Else
			pos.y = Me.ExtractAnimValue(frameIndex, posV.theAnimYValues, aBone.positionScale.y)
		End If

		If posV.animZValueOffset <= 0 Then
			pos.z = 0
		Else
			pos.z = Me.ExtractAnimValue(frameIndex, posV.theAnimZValues, aBone.positionScale.z)
		End If

		pos.debug_text = "anim"

		If (anAnimation.flags And SourceMdlAnimation.STUDIO_ANIM_DELTA) = 0 Then
			pos.x += aBone.position.x
			pos.y += aBone.position.y
			pos.z += aBone.position.z
			pos.debug_text += "+bone"
		End If

		Return pos
	End Function

	'FROM: SourceEngine2007_source\public\bone_setup.cpp
	'void ExtractAnimValue( int frame, mstudioanimvalue_t *panimvalue, float scale, float &v1 )
	'{
	'	if ( !panimvalue )
	'	{
	'		v1 = 0;
	'		return;
	'	}

	'	int k = frame;

	'	while (panimvalue->num.total <= k)
	'	{
	'		k -= panimvalue->num.total;
	'		panimvalue += panimvalue->num.valid + 1;
	'		if ( panimvalue->num.total == 0 )
	'		{
	'			Assert( 0 ); // running off the end of the animation stream is bad
	'			v1 = 0;
	'			return;
	'		}
	'	}
	'	if (panimvalue->num.valid > k)
	'	{
	'		v1 = panimvalue[k+1].value * scale;
	'	}
	'	else
	'	{
	'		// get last valid data block
	'		v1 = panimvalue[panimvalue->num.valid].value * scale;
	'	}
	'}
	Public Function ExtractAnimValue(ByVal frameIndex As Integer, ByVal animValues As List(Of SourceMdlAnimationValue), ByVal scale As Double) As Double
		Dim v1 As Double
		' k is frameCountRemainingToBeChecked
		Dim k As Integer
		Dim animValueIndex As Integer

		Try
			k = frameIndex
			animValueIndex = 0
			While animValues(animValueIndex).total <= k
				k -= animValues(animValueIndex).total
				animValueIndex += animValues(animValueIndex).valid + 1
				If animValueIndex >= animValues.Count OrElse animValues(animValueIndex).total = 0 Then
					'NOTE: Bad if it reaches here. This means maybe a newer format of the anim data was used for the model.
					v1 = 0
					Return v1
				End If
			End While

			If animValues(animValueIndex).valid > k Then
				'NOTE: Needs to be offset from current animValues index to match the C++ code above in comment.
				v1 = animValues(animValueIndex + k + 1).value * scale
			Else
				'NOTE: Needs to be offset from current animValues index to match the C++ code above in comment.
				v1 = animValues(animValueIndex + animValues(animValueIndex).valid).value * scale
			End If
		Catch
		End Try

		Return v1
	End Function

	Private Sub WriteSkeletonSectionForVertexAnimation()
		Dim line As String = ""
		'Dim timeIndex As Integer
		'Dim flexDescHasBeenWritten As List(Of Integer)
		'Dim meshVertexIndexStart As Integer

		'skeleton
		line = "skeleton"
		theOutputFileStream.WriteLine(line)

		'line = "time 0"
		'theOutputFileStream.WriteLine(line)
		'line = "time 1"
		'theOutputFileStream.WriteLine(line)
		'======
		line = "  time 0 # basis shape key"
		theOutputFileStream.WriteLine(line)

		'timeIndex = 0
		'flexDescHasBeenWritten = New List(Of Integer)
		'If theSourceEngineModel.theMdlFileHeader.theBodyParts IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theBodyParts.Count > 0 Then
		'	For bodyPartIndex As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBodyParts.Count - 1
		'		Dim aBodyPart As SourceMdlBodyPart
		'		aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex)

		'		If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
		'			For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
		'				Dim aModel As SourceMdlModel
		'				aModel = aBodyPart.theModels(modelIndex)

		'				If aModel.theMeshes IsNot Nothing AndAlso aModel.theMeshes.Count > 0 Then
		'					For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
		'						Dim aMesh As SourceMdlMesh
		'						aMesh = aModel.theMeshes(meshIndex)

		'						If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
		'							For flexIndex As Integer = 0 To aMesh.theFlexes.Count - 1
		'								Dim aFlex As SourceMdlFlex
		'								aFlex = aMesh.theFlexes(flexIndex)

		'								If flexDescHasBeenWritten.Contains(aFlex.flexDescIndex) Then
		'									Continue For
		'								Else
		'									flexDescHasBeenWritten.Add(aFlex.flexDescIndex)
		'								End If

		'								timeIndex += 1
		'								line = "time "
		'								line += timeIndex.ToString()
		'								line += " # "
		'								line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName

		'								Dim aFlexDescPartnerIndex As Integer
		'								aFlexDescPartnerIndex = aFlex.flexDescPartnerIndex
		'								If aFlexDescPartnerIndex > 0 AndAlso Not flexDescHasBeenWritten.Contains(aFlexDescPartnerIndex) Then
		'									flexDescHasBeenWritten.Add(aFlexDescPartnerIndex)
		'									line += " and "
		'									line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlexDescPartnerIndex).theName
		'								End If

		'								theOutputFileStream.WriteLine(line)
		'							Next
		'						End If
		'					Next
		'				End If
		'			Next
		'		End If
		'	Next
		'End If

		'======

		'Dim aFlexTimeStruct As FlexTimeStruct
		'Dim bodyPartVertexIndexStart As Integer
		'Dim flexDescIndexesAlreadyAdded As List(Of Integer)

		'bodyPartVertexIndexStart = 0
		'flexTimes = New List(Of FlexTimeStruct)()
		'flexDescIndexesAlreadyAdded = New List(Of Integer)()

		'If theSourceEngineModel.theMdlFileHeader.theBodyParts IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theBodyParts.Count > 0 Then
		'	For bodyPartIndex As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBodyParts.Count - 1
		'		Dim aBodyPart As SourceMdlBodyPart
		'		aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex)

		'		If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
		'			For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
		'				Dim aModel As SourceMdlModel
		'				aModel = aBodyPart.theModels(modelIndex)

		'				If aModel.theMeshes IsNot Nothing AndAlso aModel.theMeshes.Count > 0 Then
		'					For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
		'						Dim aMesh As SourceMdlMesh
		'						aMesh = aModel.theMeshes(meshIndex)

		'						meshVertexIndexStart = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex).theMeshes(meshIndex).vertexIndexStart

		'						If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
		'							For flexIndex As Integer = 0 To aMesh.theFlexes.Count - 1
		'								Dim aFlex As SourceMdlFlex
		'								aFlex = aMesh.theFlexes(flexIndex)

		'								If aFlex.theVertAnims IsNot Nothing AndAlso aFlex.theVertAnims.Count > 0 Then
		'									'If flexDescIndexesAlreadyAdded.Contains(aFlex.flexDescIndex) Then
		'									'	aFlexTimeStruct = flexTimes(flexDescIndexesAlreadyAdded.IndexOf(aFlex.flexDescIndex))

		'									'	'If aFlex.flexDescPartnerIndex = 0 Then
		'									'	aFlexTimeStruct.flexDescriptiveName += " and "
		'									'	aFlexTimeStruct.flexDescriptiveName += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
		'									'	'End If

		'									'	aFlexTimeStruct.bodyAndMeshVertexIndexStarts.Add(meshVertexIndexStart)
		'									'	aFlexTimeStruct.flexes.Add(aFlex)
		'									'Else
		'									aFlexTimeStruct = New FlexTimeStruct()
		'									aFlexTimeStruct.bodyAndMeshVertexIndexStarts = New List(Of Integer)()
		'									aFlexTimeStruct.flexes = New List(Of SourceMdlFlex)()

		'									aFlexTimeStruct.flexDescriptiveName = theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
		'									If aFlex.flexDescPartnerIndex > 0 Then
		'										aFlexTimeStruct.flexDescriptiveName += "+"
		'										aFlexTimeStruct.flexDescriptiveName += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescPartnerIndex).theName
		'									End If
		'									aFlexTimeStruct.bodyAndMeshVertexIndexStarts.Add(meshVertexIndexStart)
		'									aFlexTimeStruct.flexes.Add(aFlex)

		'									flexTimes.Add(aFlexTimeStruct)
		'									flexDescIndexesAlreadyAdded.Add(aFlex.flexDescIndex)
		'									'End If
		'								End If
		'							Next
		'						End If
		'					Next
		'				End If
		'				bodyPartVertexIndexStart += aModel.vertexCount
		'			Next
		'		End If
		'	Next
		'End If

		'Dim timeIndex As Integer
		'Dim flexTimeIndex As Integer
		'timeIndex = 1
		'For flexTimeIndex = 0 To flexTimes.Count - 1
		'	aFlexTimeStruct = flexTimes(flexTimeIndex)

		'	line = "  time "
		'	line += timeIndex.ToString()
		'	line += " # "
		'	line += aFlexTimeStruct.flexDescriptiveName
		'	theOutputFileStream.WriteLine(line)

		'	timeIndex += 1
		'Next

		''For flexIndex As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theFlexDescs.Count - 1
		''	line = "time "
		''	line += flexIndex.ToString()
		''	line += " # "
		''	line += theSourceEngineModel.theMdlFileHeader.theFlexDescs(flexIndex).theName
		''	theOutputFileStream.WriteLine(line)
		''Next

		'======

		Dim timeIndex As Integer
		Dim flexTimeIndex As Integer
		Dim aFlexFrame As FlexFrame

		timeIndex = 1
		'NOTE: The first frame was written in code above.
		For flexTimeIndex = 1 To theSourceEngineModel.theMdlFileHeader.theFlexFrames.Count - 1
			aFlexFrame = theSourceEngineModel.theMdlFileHeader.theFlexFrames(flexTimeIndex)

			line = "  time "
			line += timeIndex.ToString(TheApp.InternalNumberFormat)
			line += " # "
			line += aFlexFrame.flexDescription
			theOutputFileStream.WriteLine(line)

			timeIndex += 1
		Next

		line = "end"
		theOutputFileStream.WriteLine(line)
	End Sub

	'Private Sub WriteTriangles(ByVal lodIndex As Integer)
	'	Dim line As String = ""

	'	'triangles
	'	line = "triangles"
	'	theOutputFileStream.WriteLine(line)

	'	Dim aBodyPart As SourceVtxBodyPart
	'	Dim aModel As SourceVtxModel
	'	Dim anLod As SourceVtxModelLod
	'	Dim aMesh As SourceVtxMesh
	'	Dim aStripGroup As SourceVtxStripGroup
	'	Dim cumulativeVertexCount As Integer
	'	Dim maxIndexForMesh As Integer
	'	Dim cumulativeMaxIndex As Integer
	'	Dim materialIndex As Integer
	'	Dim materialName As String
	'	Dim meshVertexIndexStart As Integer

	'	Try
	'		If Me.theSourceEngineModel.theVtxFileHeader.theVtxBodyParts IsNot Nothing Then
	'			For bodyPartIndex As Integer = 0 To Me.theSourceEngineModel.theVtxFileHeader.theVtxBodyParts.Count - 1
	'				aBodyPart = Me.theSourceEngineModel.theVtxFileHeader.theVtxBodyParts(bodyPartIndex)

	'				If aBodyPart.theVtxModels IsNot Nothing Then
	'					For modelIndex As Integer = 0 To aBodyPart.theVtxModels.Count - 1
	'						aModel = aBodyPart.theVtxModels(modelIndex)

	'						If aModel.theVtxModelLods IsNot Nothing Then
	'							''For lodIndex As Integer = 0 To aModel.theVtxModelLods.Count - 1
	'							'Dim lodIndex As Integer = 0
	'							anLod = aModel.theVtxModelLods(lodIndex)

	'							If anLod.theVtxMeshes IsNot Nothing Then
	'								cumulativeVertexCount = 0
	'								maxIndexForMesh = 0
	'								cumulativeMaxIndex = 0
	'								For meshIndex As Integer = 0 To anLod.theVtxMeshes.Count - 1
	'									aMesh = anLod.theVtxMeshes(meshIndex)
	'									materialIndex = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex).theMeshes(meshIndex).materialIndex
	'									materialName = Me.theSourceEngineModel.theMdlFileHeader.theTextures(materialIndex).theName
	'									'materialName += ".bmp"

	'									meshVertexIndexStart = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex).theMeshes(meshIndex).vertexIndexStart

	'									If aMesh.theVtxStripGroups IsNot Nothing Then
	'										For groupIndex As Integer = 0 To aMesh.theVtxStripGroups.Count - 1
	'											aStripGroup = aMesh.theVtxStripGroups(groupIndex)

	'											If aStripGroup.theVtxStrips IsNot Nothing AndAlso aStripGroup.theVtxIndexes IsNot Nothing Then
	'												For vtxIndexIndex As Integer = 0 To aStripGroup.theVtxIndexes.Count - 3 Step 3
	'													theOutputFileStream.WriteLine(materialName)
	'													Me.WriteVertexLine(aStripGroup, vtxIndexIndex, lodIndex, meshVertexIndexStart)
	'													Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 2, lodIndex, meshVertexIndexStart)
	'													Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 1, lodIndex, meshVertexIndexStart)
	'												Next
	'												'======
	'												'For stripIndex As Integer = 0 To aStripGroup.stripCount - 1
	'												'	Dim aStrip As SourceVtxStrip = aStripGroup.theVtxStrips(stripIndex)

	'												'	For aStripIndexIndex As Integer = 0 To aStrip.indexCount - 3 Step 3
	'												'		theOutputFileStream.WriteLine(materialName)
	'												'		Me.WriteVertexLine(aStripIndexIndex + aStrip.indexMeshIndex)
	'												'		Me.WriteVertexLine(aStripIndexIndex + aStrip.indexMeshIndex + 2)
	'												'		Me.WriteVertexLine(aStripIndexIndex + aStrip.indexMeshIndex + 1)
	'												'	Next
	'												'Next
	'											End If
	'										Next
	'									End If
	'								Next
	'							End If
	'							'Next
	'						End If
	'					Next
	'				End If
	'			Next
	'		End If
	'	Catch

	'	End Try

	'	line = "end"
	'	theOutputFileStream.WriteLine(line)
	'End Sub

	Private Sub WriteTrianglesSection(ByVal aVtxModel As SourceVtxModel, ByVal lodIndex As Integer, ByVal aModel As SourceMdlModel, ByVal bodyPartVertexIndexStart As Integer)
		Dim line As String = ""

		'triangles
		line = "triangles"
		theOutputFileStream.WriteLine(line)

		Dim aVtxLod As SourceVtxModelLod
		Dim aVtxMesh As SourceVtxMesh
		Dim aStripGroup As SourceVtxStripGroup
		'Dim cumulativeVertexCount As Integer
		'Dim maxIndexForMesh As Integer
		'Dim cumulativeMaxIndex As Integer
		Dim materialIndex As Integer
		Dim materialName As String
		Dim meshVertexIndexStart As Integer

		Try
			aVtxLod = aVtxModel.theVtxModelLods(lodIndex)

			If aVtxLod.theVtxMeshes IsNot Nothing Then
				'cumulativeVertexCount = 0
				'maxIndexForMesh = 0
				'cumulativeMaxIndex = 0
				For meshIndex As Integer = 0 To aVtxLod.theVtxMeshes.Count - 1
					aVtxMesh = aVtxLod.theVtxMeshes(meshIndex)
					materialIndex = aModel.theMeshes(meshIndex).materialIndex
					materialName = Path.GetFileName(Me.theSourceEngineModel.theMdlFileHeader.theTextures(materialIndex).theName)

					meshVertexIndexStart = aModel.theMeshes(meshIndex).vertexIndexStart

					If aVtxMesh.theVtxStripGroups IsNot Nothing Then
						For groupIndex As Integer = 0 To aVtxMesh.theVtxStripGroups.Count - 1
							aStripGroup = aVtxMesh.theVtxStripGroups(groupIndex)

							If aStripGroup.theVtxStrips IsNot Nothing AndAlso aStripGroup.theVtxIndexes IsNot Nothing AndAlso aStripGroup.theVtxVertexes IsNot Nothing Then
								For vtxIndexIndex As Integer = 0 To aStripGroup.theVtxIndexes.Count - 3 Step 3
									'NOTE: studiomdl.exe will complain if texture name for eyeball is not at start of line.
									line = materialName
									theOutputFileStream.WriteLine(line)
									Me.WriteVertexLine(aStripGroup, vtxIndexIndex, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
									Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 2, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
									Me.WriteVertexLine(aStripGroup, vtxIndexIndex + 1, lodIndex, meshVertexIndexStart, bodyPartVertexIndexStart)
								Next
							End If
						Next
					End If
				Next
			End If
		Catch

		End Try

		line = "end"
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Sub WriteTrianglesSectionForCollision()
		Dim line As String = ""

		'triangles
		line = "triangles"
		theOutputFileStream.WriteLine(line)

		Dim collisionData As SourcePhyCollisionData
		Dim aBone As SourceMdlBone
		Dim aTriangle As SourcePhyFace
		'Dim aVector As SourceVector
		Dim phyVertex As SourcePhyVertex
		Dim aVectorTransformed As SourceVector
		Dim faceSection As SourcePhyFaceSection

		Try
			If Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyCollisionDatas IsNot Nothing Then
				For collisionDataIndex As Integer = 0 To Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyCollisionDatas.Count - 1
					collisionData = Me.theSourceEngineModel.thePhyFileHeader.theSourcePhyCollisionDatas(collisionDataIndex)

					For faceSectionIndex As Integer = 0 To collisionData.theFaceSections.Count - 1
						faceSection = collisionData.theFaceSections(faceSectionIndex)

						If faceSection.theBoneIndex >= Me.theSourceEngineModel.theMdlFileHeader.theBones.Count Then
							Continue For
						End If
						aBone = Me.theSourceEngineModel.theMdlFileHeader.theBones(faceSection.theBoneIndex)

						For triangleIndex As Integer = 0 To faceSection.theFaces.Count - 1
							aTriangle = faceSection.theFaces(triangleIndex)

							line = "  phy"
							theOutputFileStream.WriteLine(line)

							'NOTE: From disassembling of MDL Decompiler with OllyDbg, the following calculations are used in VPHYSICS.DLL for each face:
							'      convertedZ = 1.0 / 0.0254 * lastVertex.position.z
							'      convertedY = 1.0 / 0.0254 * -lastVertex.position.y
							'      convertedX = 1.0 / 0.0254 * lastVertex.position.x
							'NOTE: From disassembling of MDL Decompiler with OllyDbg, the following calculations are used after above for each vertex:
							'      newValue1 = unknownZ1 * convertedZ + unknownY1 * convertedY + unknownX1 * convertedX + unknownW1
							'      newValue2 = unknownZ2 * convertedZ + unknownY2 * convertedY + unknownX2 * convertedX + unknownW2
							'      newValue3 = unknownZ3 * convertedZ + unknownY3 * convertedY + unknownX3 * convertedX + unknownW3
							'Seems to be same as this code:
							'Dim aBone As SourceMdlBone
							'aBone = Me.theSourceEngineModel.theMdlFileHeader.theBones(anEyeball.boneIndex)
							'eyeballPosition = MathModule.VectorITransform(anEyeball.org, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
							'------
							'  19 -0.000009 0.000001 0.999953 0.0 0.0 0.0 1 0
							'  19 -0.000005 1.000002 -0.000043 0.0 0.0 0.0 1 0
							'  19 -0.008333 0.997005 1.003710 0.0 0.0 0.0 1 0
							'NOTE: MDL Decompiler 0.4.1 lists the vertices in reverse order than they are stored, and this seems to match closely with the teenangst source file.
							'For vertexIndex As Integer = aTriangle.vertexIndex.Length - 1 To 0 Step -1
							For vertexIndex As Integer = 0 To aTriangle.vertexIndex.Length - 1
								Dim aVector As New SourceVector()
								phyVertex = collisionData.theVertices(aTriangle.vertexIndex(vertexIndex))

								'NOTE: Works for:
								'      survivor_producer
								'NOTE: Does not work for:
								'      w_smg_uzi()
								'phyVertex.x = 1 / 0.0254 * aVector.x
								'phyVertex.y = 1 / 0.0254 * aVector.z
								'phyVertex.z = 1 / 0.0254 * -aVector.y
								'------
								'NOTE: These two lines match orientation for cstrike it_lampholder1 model, 
								'      but still doesn't compile properly.
								'NOTE: Does not work for:
								'      w_smg_uzi()
								'phyVertex.x = 1 / 0.0254 * aVector.z
								'phyVertex.y = 1 / 0.0254 * -aVector.x
								'phyVertex.z = 1 / 0.0254 * -aVector.y
								'------
								'NOTE: Does not work for:
								'      w_smg_uzi()
								'phyVertex.x = 1 / 0.0254 * aVector.x
								'phyVertex.y = 1 / 0.0254 * aVector.y
								'phyVertex.z = 1 / 0.0254 * aVector.z
								'------
								'NOTE: Does not work for:
								'      w_smg_uzi()
								'phyVertex.x = 1 / 0.0254 * aVector.y
								'phyVertex.y = 1 / 0.0254 * aVector.x
								'phyVertex.z = 1 / 0.0254 * -aVector.z
								'------
								'NOTE: Does not work for:
								'      w_smg_uzi()
								'phyVertex.x = 1 / 0.0254 * aVector.x
								'phyVertex.y = 1 / 0.0254 * aVector.y
								'phyVertex.z = 1 / 0.0254 * -aVector.z
								'------
								'NOTE: Does not work for:
								'      w_smg_uzi()
								'phyVertex.x = 1 / 0.0254 * aVector.x
								'phyVertex.y = 1 / 0.0254 * -aVector.y
								'phyVertex.z = 1 / 0.0254 * aVector.z
								'------
								'NOTE: Does not work for:
								'      w_smg_uzi()
								'phyVertex.x = 1 / 0.0254 * -aVector.y
								'phyVertex.y = 1 / 0.0254 * aVector.x
								'phyVertex.z = 1 / 0.0254 * aVector.z
								'------
								'NOTE: Does not work for:
								'      w_smg_uzi()
								'phyVertex.x = 1 / 0.0254 * -aVector.y
								'phyVertex.y = 1 / 0.0254 * aVector.x
								'phyVertex.z = 1 / 0.0254 * aVector.z
								'------
								'NOTE: Does not work for:
								'      w_smg_uzi()
								'phyVertex.x = 1 / 0.0254 * aVector.z
								'phyVertex.y = 1 / 0.0254 * aVector.y
								'phyVertex.z = 1 / 0.0254 * aVector.x
								'------
								'NOTE: Works for:
								'      w_smg_uzi()
								'NOTE: Does not work for:
								'      survivor_producer
								'phyVertex.x = 1 / 0.0254 * aVector.z
								'phyVertex.y = 1 / 0.0254 * -aVector.y
								'phyVertex.z = 1 / 0.0254 * aVector.x
								'------
								'phyVertex.x = 1 / 0.0254 * aVector.z
								'phyVertex.y = 1 / 0.0254 * -aVector.y
								'phyVertex.z = 1 / 0.0254 * -aVector.x
								'------
								'TODO: Find some rationale for why phys model is rotated differently for different models.
								'If Me.theSourceEngineModel.theMdlFileHeader.theAnimationDescs.Count < 2 Then
								If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
									'TEST: Does not rotate L4D2's van phys mesh correctly.
									'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
									'aVector.y = 1 / 0.0254 * phyVertex.vertex.y
									'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
									'TEST:  Does not rotate L4D2's van phys mesh correctly.
									'aVector.x = 1 / 0.0254 * phyVertex.vertex.y
									'aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
									'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
									'TEST: Does not rotate L4D2's van phys mesh correctly.
									'aVector.x = 1 / 0.0254 * phyVertex.vertex.z
									'aVector.y = 1 / 0.0254 * -phyVertex.vertex.y
									'aVector.z = 1 / 0.0254 * phyVertex.vertex.x
									'TEST: Does not rotate L4D2's van phys mesh correctly.
									'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
									'aVector.y = 1 / 0.0254 * phyVertex.vertex.z
									'aVector.z = 1 / 0.0254 * -phyVertex.vertex.y
									'TEST: Works for L4D2's van phys mesh.
									'      Does not work for L4D2 w_model\weapons\w_minigun.mdl.
									aVector.x = 1 / 0.0254 * phyVertex.vertex.z
									aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
									aVector.z = 1 / 0.0254 * -phyVertex.vertex.y
								Else
									'TEST: Does not work for L4D2 w_model\weapons\w_minigun.mdl.
									aVector.x = 1 / 0.0254 * phyVertex.vertex.x
									aVector.y = 1 / 0.0254 * phyVertex.vertex.z
									aVector.z = 1 / 0.0254 * -phyVertex.vertex.y
								End If
								'TEST: Does not rotate L4D2's van phys mesh correctly.
								'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
								'aVector.y = 1 / 0.0254 * phyVertex.vertex.y
								'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
								'TEST: Does not rotate L4D2's van phys mesh correctly.
								'aVector.x = 1 / 0.0254 * phyVertex.vertex.y
								'aVector.y = 1 / 0.0254 * -phyVertex.vertex.x
								'aVector.z = 1 / 0.0254 * phyVertex.vertex.z
								'TEST: works for survivor_producer; matches ref and phy meshes of van, but both are rotated 90 degrees on z-axis
								'aVector.x = 1 / 0.0254 * phyVertex.vertex.x
								'aVector.y = 1 / 0.0254 * phyVertex.vertex.z
								'aVector.z = 1 / 0.0254 * -phyVertex.vertex.y

								aVectorTransformed = MathModule.VectorITransform(aVector, aBone.poseToBoneColumn0, aBone.poseToBoneColumn1, aBone.poseToBoneColumn2, aBone.poseToBoneColumn3)
								''TEST: Only rotate by -90 deg if bone is a root bone.  Do not know why.
								'If aBone.parentBoneIndex = -1 Then
								'	aVectorTransformed = MathModule.RotateAboutZAxis(aVectorTransformed, MathModule.DegreesToRadians(-90), aBone)
								'End If

								line = "    "
								line += faceSection.theBoneIndex.ToString(TheApp.InternalNumberFormat)
								line += " "
								line += aVectorTransformed.x.ToString("0.000000", TheApp.InternalNumberFormat)
								line += " "
								line += aVectorTransformed.y.ToString("0.000000", TheApp.InternalNumberFormat)
								line += " "
								line += aVectorTransformed.z.ToString("0.000000", TheApp.InternalNumberFormat)

								'line += " 0 0 0"
								'------
								line += " "
								line += phyVertex.normal.x.ToString("0.000000", TheApp.InternalNumberFormat)
								line += " "
								line += phyVertex.normal.y.ToString("0.000000", TheApp.InternalNumberFormat)
								line += " "
								line += phyVertex.normal.z.ToString("0.000000", TheApp.InternalNumberFormat)

								line += " 0 0"
								'NOTE: The studiomdl.exe doesn't need the integer values at end.
								'line += " 1 0"
								theOutputFileStream.WriteLine(line)
							Next
						Next
					Next
				Next
			End If
		Catch

		End Try

		line = "end"
		theOutputFileStream.WriteLine(line)
	End Sub

	'Private Sub WriteVertexAnimation(ByVal aFlex As SourceMdlFlex)
	'	Dim line As String = ""
	'	Dim aVertex As SourceVertex
	'	Dim positionX As Double
	'	Dim positionY As Double
	'	Dim positionZ As Double
	'	Dim normalX As Double
	'	Dim normalY As Double
	'	Dim normalZ As Double

	'	'vertexanimation
	'	line = "vertexanimation"
	'	theOutputFileStream.WriteLine(line)

	'	line = "time 0"
	'	theOutputFileStream.WriteLine(line)

	'	For i As Integer = 0 To Me.theSourceEngineModel.theVvdFileHeader.theVertexes.Count - 1
	'		aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(i)
	'		positionX = aVertex.positionX
	'		positionY = aVertex.positionY
	'		positionZ = aVertex.positionZ
	'		normalX = aVertex.normalX
	'		normalY = aVertex.normalY
	'		normalZ = aVertex.normalZ
	'		line = i.ToString()
	'		line += " "
	'		line += positionX.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += positionY.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += positionZ.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += normalX.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += normalY.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += normalZ.ToString("0.000000", TheApp.InternalNumberFormat)
	'	Next

	'	line = "time 1"
	'	theOutputFileStream.WriteLine(line)

	'	For i As Integer = 0 To aFlex.theVertAnims.Count - 1
	'		Dim aVertAnim As SourceMdlVertAnim
	'		aVertAnim = aFlex.theVertAnims(i)

	'		'line = "time "
	'		'line += currentVertAnimIndex.ToString()
	'		'theOutputFileStream.WriteLine(line)

	'		'line = aVertAnim.index.ToString()
	'		'line += " "
	'		'line += aVertAnim.flDelta(0).TheFloatValue.ToString("0.000000", TheApp.InternalNumberFormat)
	'		'line += " "
	'		'line += aVertAnim.flDelta(1).TheFloatValue.ToString("0.000000", TheApp.InternalNumberFormat)
	'		'line += " "
	'		'line += aVertAnim.flDelta(2).TheFloatValue.ToString("0.000000", TheApp.InternalNumberFormat)
	'		'line += " "
	'		'line += aVertAnim.flNDelta(0).TheFloatValue.ToString("0.000000", TheApp.InternalNumberFormat)
	'		'line += " "
	'		'line += aVertAnim.flNDelta(1).TheFloatValue.ToString("0.000000", TheApp.InternalNumberFormat)
	'		'line += " "
	'		'line += aVertAnim.flNDelta(2).TheFloatValue.ToString("0.000000", TheApp.InternalNumberFormat)
	'		'======
	'		' Write calculated values for each vertex animation line.
	'		aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(aVertAnim.index)
	'		positionX = aVertex.positionX + aVertAnim.flDelta(0).TheFloatValue
	'		positionY = aVertex.positionY + aVertAnim.flDelta(1).TheFloatValue
	'		positionZ = aVertex.positionZ + aVertAnim.flDelta(2).TheFloatValue
	'		normalX = aVertex.normalX + aVertAnim.flNDelta(0).TheFloatValue
	'		normalY = aVertex.normalY + aVertAnim.flNDelta(1).TheFloatValue
	'		normalZ = aVertex.normalZ + aVertAnim.flNDelta(2).TheFloatValue
	'		line = aVertAnim.index.ToString()
	'		line += " "
	'		line += positionX.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += positionY.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += positionZ.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += normalX.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += normalY.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += normalZ.ToString("0.000000", TheApp.InternalNumberFormat)

	'		' For debugging.
	'		'line += " // "
	'		'line += aVertAnim.flDelta(0).the16BitValue.ToString()
	'		'line += " "
	'		'line += aVertAnim.flDelta(1).the16BitValue.ToString()
	'		'line += " "
	'		'line += aVertAnim.flDelta(2).the16BitValue.ToString()
	'		'line += " "
	'		'line += aVertAnim.flNDelta(0).the16BitValue.ToString()
	'		'line += " "
	'		'line += aVertAnim.flNDelta(1).the16BitValue.ToString()
	'		'line += " "
	'		'line += aVertAnim.flNDelta(2).the16BitValue.ToString()

	'		theOutputFileStream.WriteLine(line)
	'	Next

	'	line = "end"
	'	theOutputFileStream.WriteLine(line)
	'End Sub

	Private Sub WriteVertexAnimationSection()
		Dim line As String = ""
		'Dim aVtxBodyPart As SourceVtxBodyPart
		'Dim aVtxModel As SourceVtxModel
		'Dim aVtxLod As SourceVtxModelLod
		'Dim aVtxMesh As SourceVtxMesh
		'Dim aVtxStripGroup As SourceVtxStripGroup
		'Dim cumulativeVertexCount As Integer
		'Dim maxIndexForMesh As Integer
		'Dim cumulativeMaxIndex As Integer
		'Dim meshVertexIndexStart As Integer
		'Dim vertexIndex As Integer
		'Dim mappedVertexIndex As Integer
		'Dim aVertex As SourceVertex
		'Dim positionX As Double
		'Dim positionY As Double
		'Dim positionZ As Double
		'Dim normalX As Double
		'Dim normalY As Double
		'Dim normalZ As Double

		'vertexanimation
		line = "vertexanimation"
		theOutputFileStream.WriteLine(line)

		line = "  time 0 # basis shape key"
		theOutputFileStream.WriteLine(line)

		'Try
		'	If Me.theSourceEngineModel.theVtxFileHeader.theVtxBodyParts IsNot Nothing Then
		'		vertexIndex = 0
		'		For vtxBodyPartIndex As Integer = 0 To Me.theSourceEngineModel.theVtxFileHeader.theVtxBodyParts.Count - 1
		'			aVtxBodyPart = Me.theSourceEngineModel.theVtxFileHeader.theVtxBodyParts(vtxBodyPartIndex)

		'			If aVtxBodyPart.theVtxModels IsNot Nothing Then
		'				For vtxModelIndex As Integer = 0 To aVtxBodyPart.theVtxModels.Count - 1
		'					aVtxModel = aVtxBodyPart.theVtxModels(vtxModelIndex)

		'					If aVtxModel.theVtxModelLods IsNot Nothing Then
		'						''For lodIndex As Integer = 0 To aModel.theVtxModelLods.Count - 1
		'						Dim vtxLodIndex As Integer = 0
		'						aVtxLod = aVtxModel.theVtxModelLods(vtxLodIndex)

		'						If aVtxLod.theVtxMeshes IsNot Nothing Then
		'							cumulativeVertexCount = 0
		'							maxIndexForMesh = 0
		'							cumulativeMaxIndex = 0
		'							For vtxMeshIndex As Integer = 0 To aVtxLod.theVtxMeshes.Count - 1
		'								aVtxMesh = aVtxLod.theVtxMeshes(vtxMeshIndex)

		'								meshVertexIndexStart = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(vtxBodyPartIndex).theModels(vtxModelIndex).theMeshes(vtxMeshIndex).vertexIndexStart

		'								If aVtxMesh.theVtxStripGroups IsNot Nothing Then
		'									For vtxStripGroupIndex As Integer = 0 To aVtxMesh.theVtxStripGroups.Count - 1
		'										aVtxStripGroup = aVtxMesh.theVtxStripGroups(vtxStripGroupIndex)

		'										If aVtxStripGroup.theVtxStrips IsNot Nothing AndAlso aVtxStripGroup.theVtxIndexes IsNot Nothing Then
		'											For vtxIndexIndex As Integer = 0 To aVtxStripGroup.theVtxIndexes.Count - 3 Step 3
		'												Me.WriteBasisVertexAnimLine(aVtxStripGroup, vtxIndexIndex, vertexIndex, meshVertexIndexStart)
		'												vertexIndex += 1
		'												Me.WriteBasisVertexAnimLine(aVtxStripGroup, vtxIndexIndex + 2, vertexIndex, meshVertexIndexStart)
		'												vertexIndex += 1
		'												Me.WriteBasisVertexAnimLine(aVtxStripGroup, vtxIndexIndex + 1, vertexIndex, meshVertexIndexStart)
		'												vertexIndex += 1
		'											Next
		'											'======
		'											'For stripIndex As Integer = 0 To aStripGroup.stripCount - 1
		'											'	Dim aStrip As SourceVtxStrip = aStripGroup.theVtxStrips(stripIndex)

		'											'	For aStripIndexIndex As Integer = 0 To aStrip.indexCount - 3 Step 3
		'											'		theOutputFileStream.WriteLine(materialName)
		'											'		Me.WriteVertexLine(aStripIndexIndex + aStrip.indexMeshIndex)
		'											'		Me.WriteVertexLine(aStripIndexIndex + aStrip.indexMeshIndex + 2)
		'											'		Me.WriteVertexLine(aStripIndexIndex + aStrip.indexMeshIndex + 1)
		'											'	Next
		'											'Next
		'										End If
		'									Next
		'								End If
		'							Next
		'						End If
		'						'Next
		'					End If
		'				Next
		'			End If
		'		Next
		'	End If
		'Catch
		'End Try

		'======

		Try
			Dim aVertex As SourceVertex
			For vertexIndex As Integer = 0 To Me.theSourceEngineModel.theVvdFileHeader.theVertexes.Count - 1
				If Me.theSourceEngineModel.theVvdFileHeader.fixupCount = 0 Then
					aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(vertexIndex)
				Else
					'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
					'      Maybe the listing by lodIndex is only needed internally by graphics engine.
					aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(0)(vertexIndex)
				End If

				line = "    "
				line += vertexIndex.ToString(TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.positionX.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.positionY.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.positionZ.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.normalX.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.normalY.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.normalZ.ToString("0.000000", TheApp.InternalNumberFormat)
				theOutputFileStream.WriteLine(line)
			Next
		Catch
		End Try

		'######

		'Dim flexTimes As List(Of FlexTimeStruct)
		'Dim aFlexTimeStruct As FlexTimeStruct
		'Dim aFlexPartnerIndex As Integer

		'flexTimes = New List(Of FlexTimeStruct)()
		'For i As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theFlexDescs.Count - 1
		'	aFlexTimeStruct = New FlexTimeStruct()
		'	'aFlexTimeStruct.isValid = False
		'	aFlexTimeStruct.meshVertexIndexStarts = New List(Of Integer)()
		'	aFlexTimeStruct.flexes = New List(Of SourceMdlFlex)()
		'	flexTimes.Add(aFlexTimeStruct)
		'Next

		'flexTimeIndex = 0
		'If theSourceEngineModel.theMdlFileHeader.theBodyParts IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theBodyParts.Count > 0 Then
		'	For bodyPartIndex As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBodyParts.Count - 1
		'		Dim aBodyPart As SourceMdlBodyPart
		'		aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex)
		'		'Dim aVtxBodyPart As SourceVtxBodyPart
		'		'aVtxBodyPart = TheSourceEngineModel.theVtxFileHeader.theVtxBodyParts(bodyPartIndex)

		'		If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
		'			For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
		'				Dim aModel As SourceMdlModel
		'				aModel = aBodyPart.theModels(modelIndex)
		'				'Dim aVtxModel As SourceVtxModel
		'				'aVtxModel = aVtxBodyPart.theVtxModels(modelIndex)

		'				If aModel.theMeshes IsNot Nothing AndAlso aModel.theMeshes.Count > 0 Then
		'					For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
		'						Dim aMesh As SourceMdlMesh
		'						aMesh = aModel.theMeshes(meshIndex)
		'						'Dim aVtxMesh As SourceVtxMesh
		'						'aVtxMesh = aVtxModel.theVtxModelLods(0).theVtxMeshes(meshIndex)

		'						meshVertexIndexStart = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex).theMeshes(meshIndex).vertexIndexStart

		'						If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
		'							For flexIndex As Integer = 0 To aMesh.theFlexes.Count - 1
		'								Dim aFlex As SourceMdlFlex
		'								aFlex = aMesh.theFlexes(flexIndex)

		'								If aFlex.theVertAnims IsNot Nothing AndAlso aFlex.theVertAnims.Count > 0 Then
		'									aFlexTimeStruct = flexTimes(aFlex.flexDescIndex)
		'									aFlexPartnerIndex = aFlex.flexDescPartnerIndex
		'									'If Not aFlexTimeStruct.isValid Then
		'									If aFlexTimeStruct.flexes.Count > 0 Then
		'										'NOTE: More than one flex can point to same flexDesc.
		'										'      This might only occur for eyelids.
		'										If aFlexPartnerIndex = 0 Then
		'											aFlexTimeStruct.flexDescriptiveName += " and "
		'											aFlexTimeStruct.flexDescriptiveName += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
		'										End If
		'									Else
		'										'aFlexTimeStruct.isValid = True
		'										aFlexTimeStruct.flexDescriptiveName = theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
		'									End If
		'									aFlexTimeStruct.meshVertexIndexStarts.Add(meshVertexIndexStart)
		'									aFlexTimeStruct.flexes.Add(aFlex)

		'									'If aFlexPartnerIndex > 0 AndAlso aFlexTimeStruct.isValid Then
		'									If aFlexPartnerIndex > 0 AndAlso aFlexTimeStruct.flexes.Count > 0 Then
		'										'NOTE: A partner flex should be added to same flexTimeStruct.
		'										aFlexTimeStruct = flexTimes(aFlex.flexDescIndex)
		'										'If aFlexTimeStruct.flexes.Count < 2 Then
		'										aFlexTimeStruct.flexDescriptiveName += " and "
		'										aFlexTimeStruct.flexDescriptiveName += theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlexPartnerIndex).theName
		'										'End If
		'										aFlexTimeStruct.meshVertexIndexStarts.Add(meshVertexIndexStart)
		'										aFlexTimeStruct.flexes.Add(aFlex)
		'									End If
		'								End If
		'							Next
		'						End If
		'					Next
		'				End If
		'			Next
		'		End If
		'	Next
		'End If

		'Dim timeIndex As Integer
		'timeIndex = 1
		'For flexTimeIndex = 0 To flexTimes.Count - 1
		'	aFlexTimeStruct = flexTimes(flexTimeIndex)

		'	'If aFlexTimeStruct.isValid Then
		'	If aFlexTimeStruct.flexes.Count > 0 Then
		'		line = "time "
		'		line += timeIndex.ToString()
		'		line += " # "
		'		line += aFlexTimeStruct.flexDescriptiveName
		'		theOutputFileStream.WriteLine(line)

		'		For x As Integer = 0 To aFlexTimeStruct.flexes.Count - 1
		'			Me.WriteVertexAnimLines(aFlexTimeStruct.flexes(x), aFlexTimeStruct.meshVertexIndexStarts(x))
		'		Next

		'		timeIndex += 1
		'	End If
		'Next

		'======

		'Dim flexTimes As List(Of FlexTimeStruct)
		'Dim aFlexTimeStruct As FlexTimeStruct
		'Dim bodyPartVertexIndexStart As Integer

		'bodyPartVertexIndexStart = 0
		'flexTimes = New List(Of FlexTimeStruct)()

		'If theSourceEngineModel.theMdlFileHeader.theBodyParts IsNot Nothing AndAlso theSourceEngineModel.theMdlFileHeader.theBodyParts.Count > 0 Then
		'	For bodyPartIndex As Integer = 0 To theSourceEngineModel.theMdlFileHeader.theBodyParts.Count - 1
		'		Dim aBodyPart As SourceMdlBodyPart
		'		aBodyPart = theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex)

		'		If aBodyPart.theModels IsNot Nothing AndAlso aBodyPart.theModels.Count > 0 Then
		'			For modelIndex As Integer = 0 To aBodyPart.theModels.Count - 1
		'				Dim aModel As SourceMdlModel
		'				aModel = aBodyPart.theModels(modelIndex)

		'				If aModel.theMeshes IsNot Nothing AndAlso aModel.theMeshes.Count > 0 Then
		'					For meshIndex As Integer = 0 To aModel.theMeshes.Count - 1
		'						Dim aMesh As SourceMdlMesh
		'						aMesh = aModel.theMeshes(meshIndex)

		'						meshVertexIndexStart = Me.theSourceEngineModel.theMdlFileHeader.theBodyParts(bodyPartIndex).theModels(modelIndex).theMeshes(meshIndex).vertexIndexStart

		'						If aMesh.theFlexes IsNot Nothing AndAlso aMesh.theFlexes.Count > 0 Then
		'							For flexIndex As Integer = 0 To aMesh.theFlexes.Count - 1
		'								Dim aFlex As SourceMdlFlex
		'								aFlex = aMesh.theFlexes(flexIndex)

		'								If aFlex.theVertAnims IsNot Nothing AndAlso aFlex.theVertAnims.Count > 0 Then
		'									aFlexTimeStruct = New FlexTimeStruct()

		'									aFlexTimeStruct.flexDescriptiveName = theSourceEngineModel.theMdlFileHeader.theFlexDescs(aFlex.flexDescIndex).theName
		'									aFlexTimeStruct.bodyAndMeshVertexIndexStart = meshVertexIndexStart
		'									aFlexTimeStruct.flex = aFlex

		'									flexTimes.Add(aFlexTimeStruct)
		'								End If
		'							Next
		'						End If
		'					Next
		'				End If
		'				bodyPartVertexIndexStart += aModel.vertexCount
		'			Next
		'		End If
		'	Next
		'End If

		'Dim timeIndex As Integer
		'timeIndex = 1
		'For flexTimeIndex = 0 To flexTimes.Count - 1
		'	aFlexTimeStruct = flexTimes(flexTimeIndex)

		'	line = "time "
		'	line += timeIndex.ToString()
		'	line += " # "
		'	line += aFlexTimeStruct.flexDescriptiveName
		'	theOutputFileStream.WriteLine(line)

		'	Me.WriteVertexAnimLines(aFlexTimeStruct.flex, aFlexTimeStruct.bodyAndMeshVertexIndexStart)

		'	timeIndex += 1
		'Next

		'======

		'Dim timeIndex As Integer
		'Dim flexTimeIndex As Integer
		'Dim aFlexTimeStruct As FlexTimeStruct

		'timeIndex = 1
		'For flexTimeIndex = 0 To flexTimes.Count - 1
		'	aFlexTimeStruct = flexTimes(flexTimeIndex)

		'	line = "  time "
		'	line += timeIndex.ToString()
		'	line += " # "
		'	line += aFlexTimeStruct.flexDescriptiveName
		'	theOutputFileStream.WriteLine(line)

		'	For x As Integer = 0 To aFlexTimeStruct.flexes.Count - 1
		'		Me.WriteVertexAnimLines(aFlexTimeStruct.flexes(x), aFlexTimeStruct.bodyAndMeshVertexIndexStarts(x))
		'	Next

		'	timeIndex += 1
		'Next

		'======

		Dim timeIndex As Integer
		Dim flexTimeIndex As Integer
		Dim aFlexFrame As FlexFrame

		timeIndex = 1
		'NOTE: The first frame was written in code above.
		For flexTimeIndex = 1 To theSourceEngineModel.theMdlFileHeader.theFlexFrames.Count - 1
			aFlexFrame = theSourceEngineModel.theMdlFileHeader.theFlexFrames(flexTimeIndex)

			line = "  time "
			line += timeIndex.ToString(TheApp.InternalNumberFormat)
			line += " # "
			line += aFlexFrame.flexDescription
			theOutputFileStream.WriteLine(line)

			For x As Integer = 0 To aFlexFrame.flexes.Count - 1
				Me.WriteVertexAnimLines(aFlexFrame.flexes(x), aFlexFrame.bodyAndMeshVertexIndexStarts(x))
			Next

			timeIndex += 1
		Next

		line = "end"
		theOutputFileStream.WriteLine(line)
	End Sub

	Private Class FlexTimeStruct
		'Public isValid As Boolean
		Public flexDescriptiveName As String
		'Public meshVertexIndexStarts As List(Of Integer)
		Public bodyAndMeshVertexIndexStarts As List(Of Integer)
		Public flexes As List(Of SourceMdlFlex)
	End Class

	'Private Class FlexTimeStruct
	'	'Public isValid As Boolean
	'	Public flexDescriptiveName As String
	'	Public bodyAndMeshVertexIndexStart As Integer
	'	Public flex As SourceMdlFlex
	'End Class

	Private Sub WriteVertexLine(ByVal aStripGroup As SourceVtxStripGroup, ByVal aVtxIndexIndex As Integer, ByVal lodIndex As Integer, ByVal meshVertexIndexStart As Integer, ByVal bodyPartVertexIndexStart As Integer)
		Dim aVtxVertexIndex As UShort
		Dim aVtxVertex As SourceVtxVertex
		Dim aVertex As SourceVertex
		Dim vertexIndex As Integer
		Dim line As String

		Try
			aVtxVertexIndex = aStripGroup.theVtxIndexes(aVtxIndexIndex)
			aVtxVertex = aStripGroup.theVtxVertexes(aVtxVertexIndex)
			vertexIndex = aVtxVertex.originalMeshVertexIndex + bodyPartVertexIndexStart + meshVertexIndexStart
			If Me.theSourceEngineModel.theVvdFileHeader.fixupCount = 0 Then
				aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(vertexIndex)
			Else
				'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
				'      Maybe the listing by lodIndex is only needed internally by graphics engine.
				'aVertex = Me.theSourceEngineModel.theVvdFileData.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
				aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(0)(vertexIndex)
				'aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
			End If

			line = "  "
			line += aVertex.boneWeight.bone(0).ToString(TheApp.InternalNumberFormat)

			line += " "
			If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
				'TODO: This does not work for L4D2 w_models\weapons\w_minigun.mdl.
				line += aVertex.positionY.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += (-aVertex.positionX).ToString("0.000000", TheApp.InternalNumberFormat)
			Else
				'TODO: This does not work for L4D2 w_models\weapons\w_minigun.mdl.
				line += aVertex.positionX.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.positionY.ToString("0.000000", TheApp.InternalNumberFormat)
			End If
			line += " "
			line += aVertex.positionZ.ToString("0.000000", TheApp.InternalNumberFormat)

			line += " "
			If (theSourceEngineModel.theMdlFileHeader.flags And SourceMdlFileHeader.STUDIOHDR_FLAGS_STATIC_PROP) > 0 Then
				line += aVertex.normalY.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += (-aVertex.normalX).ToString("0.000000", TheApp.InternalNumberFormat)
			Else
				line += aVertex.normalX.ToString("0.000000", TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.normalY.ToString("0.000000", TheApp.InternalNumberFormat)
			End If
			line += " "
			line += aVertex.normalZ.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += aVertex.texCoordX.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			'line += aVertex.texCoordY.ToString("0.000000", TheApp.InternalNumberFormat)
			line += (-aVertex.texCoordY).ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += aVertex.boneWeight.boneCount.ToString(TheApp.InternalNumberFormat)
			For boneIndex As Integer = 0 To aVertex.boneWeight.boneCount - 1
				line += " "
				line += aVertex.boneWeight.bone(boneIndex).ToString(TheApp.InternalNumberFormat)
				line += " "
				line += aVertex.boneWeight.weight(boneIndex).ToString("0.000000", TheApp.InternalNumberFormat)
			Next
			theOutputFileStream.WriteLine(line)
		Catch
		End Try
	End Sub

	'Private Sub WriteBasisVertexAnimLine(ByVal aStripGroup As SourceVtxStripGroup, ByVal aVtxIndexIndex As Integer, ByVal vertexIndex As Integer, ByVal meshVertexIndexStart As Integer)
	'	Dim aVtxVertexIndex As UShort
	'	Dim aVtxVertex As SourceVtxVertex
	'	Dim aVertex As SourceVertex
	'	Dim line As String

	'	Try
	'		aVtxVertexIndex = aStripGroup.theVtxIndexes(aVtxIndexIndex)
	'		aVtxVertex = aStripGroup.theVtxVertexes(aVtxVertexIndex)
	'		If Me.theSourceEngineModel.theVvdFileHeader.fixupCount = 0 Then
	'			aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
	'			'aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(vertexIndex)
	'		Else
	'			'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
	'			'      Maybe the listing by lodIndex is only needed internally by graphics engine.
	'			'aVertex = Me.theSourceEngineModel.theVvdFileData.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
	'			aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(0)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
	'			'aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(0)(vertexIndex)
	'		End If

	'		''NOTE: Add the vertex to a list to be accessed by index for subsequent VTA anim writing.
	'		'Me.theVtaVertexes.Add(aVertex)

	'		line = vertexIndex.ToString()
	'		line += " "
	'		line += aVertex.positionX.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += aVertex.positionY.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += aVertex.positionZ.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += aVertex.normalX.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += aVertex.normalY.ToString("0.000000", TheApp.InternalNumberFormat)
	'		line += " "
	'		line += aVertex.normalZ.ToString("0.000000", TheApp.InternalNumberFormat)
	'		theOutputFileStream.WriteLine(line)
	'	Catch
	'	End Try
	'End Sub

	Private Sub WriteVertexAnimLines(ByVal aFlex As SourceMdlFlex, ByVal bodyAndMeshVertexIndexStart As Integer)
		Dim line As String
		Dim aVertex As SourceVertex
		Dim vertexIndex As Integer
		'Dim mappedVertexIndex As Integer
		Dim positionX As Double
		Dim positionY As Double
		Dim positionZ As Double
		Dim normalX As Double
		Dim normalY As Double
		Dim normalZ As Double

		For i As Integer = 0 To aFlex.theVertAnims.Count - 1
			Dim aVertAnim As SourceMdlVertAnim
			aVertAnim = aFlex.theVertAnims(i)

			'TODO: Figure out why decompiling teen angst zoey (which has 39 shape keys) gives 55 shapekeys.
			'      - Probably extra ones are related to flexpairs (right and left).
			'      - Eyelids are combined, e.g. second shapekey from source vta is upper_lid_lowerer
			'        that contains both upper_right_lowerer and upper_left_lowerer.

			'NOTE: Figure out which list of vertexes to index.
			'aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(aVertAnim.index)
			'vertexIndex = aVertAnim.index
			'======
			'aVertex = Me.theVtaVertexes(aVertAnim.index)
			'vertexIndex = aVertAnim.index
			'======
			'NOTE: This list of vertexes works; it imports into Blender correctly.
			vertexIndex = aVertAnim.index + bodyAndMeshVertexIndexStart
			'vertexIndex = aVertAnim.index
			If Me.theSourceEngineModel.theVvdFileHeader.fixupCount = 0 Then
				aVertex = Me.theSourceEngineModel.theVvdFileHeader.theVertexes(vertexIndex)
			Else
				'NOTE: I don't know why lodIndex is not needed here, but using only lodIndex=0 matches what MDL Decompiler produces.
				'      Maybe the listing by lodIndex is only needed internally by graphics engine.
				'aVertex = Me.theSourceEngineModel.theVvdFileData.theFixedVertexesByLod(lodIndex)(aVtxVertex.originalMeshVertexIndex + meshVertexIndexStart)
				aVertex = Me.theSourceEngineModel.theVvdFileHeader.theFixedVertexesByLod(0)(vertexIndex)
			End If
			'mappedVertexIndex = Me.theVtaVertexes.IndexOf(aVertex)
			'If mappedVertexIndex < 0 Then
			'	mappedVertexIndex = 0
			'End If

			positionX = aVertex.positionX + aVertAnim.flDelta(0).TheFloatValue
			positionY = aVertex.positionY + aVertAnim.flDelta(1).TheFloatValue
			positionZ = aVertex.positionZ + aVertAnim.flDelta(2).TheFloatValue
			normalX = aVertex.normalX + aVertAnim.flNDelta(0).TheFloatValue
			normalY = aVertex.normalY + aVertAnim.flNDelta(1).TheFloatValue
			normalZ = aVertex.normalZ + aVertAnim.flNDelta(2).TheFloatValue
			'NOTE: This matches values given by MDL Decompiler 0.5.
			'line = aVertAnim.index.ToString()
			line = "    "
			line += vertexIndex.ToString(TheApp.InternalNumberFormat)
			'line = mappedVertexIndex.ToString()
			line += " "
			line += positionX.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += positionY.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += positionZ.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += normalX.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += normalY.ToString("0.000000", TheApp.InternalNumberFormat)
			line += " "
			line += normalZ.ToString("0.000000", TheApp.InternalNumberFormat)

			'TEST:
			'If aFlex.vertAnimType = aFlex.STUDIO_VERT_ANIM_WRINKLE Then
			'	CType(aVertAnim, SourceMdlVertAnimWrinkle).wrinkleDelta = Me.theInputFileReader.ReadInt16()
			'End If
			'If blah Then
			'	line += " // wrinkle value: "
			'	line += aVertAnim.flDelta(0).the16BitValue.ToString()
			'End If

			' For debugging.
			'line += " // "
			'line += aVertAnim.flDelta(0).the16BitValue.ToString()
			'line += " "
			'line += aVertAnim.flDelta(1).the16BitValue.ToString()
			'line += " "
			'line += aVertAnim.flDelta(2).the16BitValue.ToString()
			'line += " "
			'line += aVertAnim.flNDelta(0).the16BitValue.ToString()
			'line += " "
			'line += aVertAnim.flNDelta(1).the16BitValue.ToString()
			'line += " "
			'line += aVertAnim.flNDelta(2).the16BitValue.ToString()

			theOutputFileStream.WriteLine(line)
		Next
	End Sub

#End Region

#Region "Data"

	Private theSourceEngineModel As SourceModel
	Private theOutputFileStream As StreamWriter

	Private theWeaponBoneIndex As Integer

	'Private theAnimationFramePositions As List(Of SourceVector)
	'Private theAnimationFrameRotations As List(Of SourceQuaternion)
	Private theAnimationFrameLines As SortedList(Of Integer, AnimationFrameLine)

	'Private flexTimes As List(Of FlexTimeStruct)

	'Private theVtaVertexes As List(Of SourceVertex)

#End Region

End Class
