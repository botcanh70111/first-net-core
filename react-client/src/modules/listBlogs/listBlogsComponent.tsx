import React, { FunctionComponentElement, useEffect } from 'react';
import { connect } from 'react-redux';
import { Dispatch, AnyAction, bindActionCreators, ActionCreator } from 'redux';
import { BlogModel } from '../blog/blogModel';
import { RootState } from '../reducers';
import { getBlog } from '../blog/blogActions';
import { getListBlogs } from './listBlogsActions';
import { Hosts } from '../../constants/hosts';
import { Link } from 'react-router-dom';

interface ListBlogsComponentProps {
    blogs: BlogModel[],
    getList: ActionCreator<any>
}

const ListBlogsComponent = (props: ListBlogsComponentProps): FunctionComponentElement<ListBlogsComponentProps> => {
    useEffect(() => {
        props.getList();
    }, []);

    return (
        <div className="row">
            {props.blogs.length > 0 &&
                props.blogs.map((b, i) => {
                    const url = `/blogs/${b.blogUrl}`;
                    return (
                        <div className="col-md-4 col-sm-12" key={i}>
                            <div className="promo-item">
                                <img className="promo-item__image" src={Hosts.BaseUrl + b.imageUrl} alt="" />
                                <Link className="promo-item__button" to={url}>Click here</Link>
                                {/* <button className="promo-item__button">
                                    Click here
                                </button> */}
                            </div>
                        </div>
                    );
                }
                )}
        </div>
    );
}


const mapStateToProps = (state: RootState) => {
    return {
        blogs: state.listBlogs
    }
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
    return bindActionCreators({
        getList: getListBlogs
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(ListBlogsComponent);