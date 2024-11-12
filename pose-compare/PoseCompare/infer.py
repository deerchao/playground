import numpy as np
import mediapipe as mp
from collections.abc import Buffer

mp_pose = mp.solutions.pose
pose = mp_pose.Pose()

def mediapipe_body_pose(width: int, height: int, channel: int, buffer: Buffer) -> list[float]:

    frame = np.ndarray((height, width, channel), np.uint8, buffer)

    keypoints: list[float] = []
    results = pose.process(frame)
    if results.pose_landmarks:
        for _, landmark in enumerate(results.pose_landmarks.landmark):
            keypoints.append(landmark.x)
            keypoints.append(landmark.y)

    clone = frame.copy()

    return keypoints


def mediapipe_body_pose_3d(width: int, height: int, channel: int, buffer: Buffer) -> list[float]:

    frame = np.ndarray((height, width, channel), np.uint8, buffer)

    keypoints: list[float] = []
    results = pose.process(frame)
    if results.pose_world_landmarks:
        for _, landmark in enumerate(results.pose_world_landmarks.landmark):
            keypoints.append(landmark.x)
            keypoints.append(landmark.y)
            keypoints.append(landmark.z)

    clone = frame.copy()

    return keypoints
