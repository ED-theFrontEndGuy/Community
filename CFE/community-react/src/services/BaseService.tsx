import axios from 'axios';
import axiosInstance from './axiosInstance';

export abstract class BaseService {
	protected axiosInstance = axiosInstance;
}
