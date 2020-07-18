import React, { FunctionComponentElement, useEffect } from "react";
import { BlogInfo } from "./blogModel";
import { useParams } from "react-router-dom";
import { RootState } from "../reducers";
import {Dispatch, bindActionCreators, AnyAction, ActionCreator} from 'redux';
import {connect} from 'react-redux';
import { getBlog } from "./blogActions";
import ReactHtmlParser from 'react-html-parser'; 
import { Hosts } from "../../constants/hosts";

interface BlogComponentProps {
    blog: BlogInfo,
    getDetail: ActionCreator<any>,
}

const BlogComponent = (props: BlogComponentProps) : FunctionComponentElement<BlogComponentProps> => {
    const { slug } = useParams();
    useEffect(() => {
        props.getDetail(slug);
    }, [slug]);

    const template = props.blog !== undefined && props.blog.blog !== undefined ?
    <div className="row">
        <div className="col-xs-12">
            <div className="blog-page">
                {props.blog.blog.imageUrl &&
                    <div className="blog-image">
                        <img src={Hosts.BaseUrl + props.blog.blog.imageUrl} alt="" />
                    </div>}

                <div className="blog-content">
                    <div className="item-tag">
                        {props.blog.category !== null && <a href={props.blog.category.categoryUrl}>{props.blog.category.name}</a>}
                    </div>
                    <h2 className="title">{props.blog.blog.name}</h2>
                    <div className="item-info">
                        <span>By </span>
                        <a href="#"> {props.blog.author !== undefined && props.blog.author.firstName + " " + props.blog.author.lastName}</a>
                        <span>{props.blog.blog.created.toLocaleDateString}</span>
                        <span>Edited at {props.blog.blog.edited.toLocaleDateString}</span>
                    </div>

                    <div className="blog-post">
                        {ReactHtmlParser(props.blog.blog.content)}
                    </div>

                    <div className="blog-tags">
                        {props.blog.tags.length > 0 && 
                            props.blog.tags.map((t) => <a href={t.tagUrl} className="tag">{t.name}</a>)
                        }
                    </div>
                </div>

                <div className="blog-social flex-between">
                    <div>
                        <a href="">0 comments</a>
                        <span>2.1K views</span>
                    </div>
                    <div className="blog-share">
                        <span>Share: </span>
                        <a href=""><i className="fab fa-facebook-square"></i></a>
                        <a href=""><i className="fab fa-linkedin"></i></a>
                        <a href=""><i className="fab fa-twitter-square"></i></a>
                        <a href=""><i className="fas fa-envelope"></i></a>
                    </div>
                </div>

                <div className="blog-sign">
                    <div className="avatar">
                        <img src="blog.Author.Avatar" alt="" />
                    </div>
                    <div className="blog-sign--content">
                        <h4 className="title">{props.blog.author.firstName} {props.blog.author.lastName}</h4>
                        <p>{props.blog.blog.postScript}</p>
                        <div className="blog-share">
                            <a href=""><i className="fab fa-facebook-square"></i></a>
                            <a href=""><i className="fab fa-linkedin"></i></a>
                            <a href=""><i className="fab fa-twitter-square"></i></a>
                            <a href=""><i className="fas fa-envelope"></i></a>
                        </div>
                    </div>
                </div>

                <div className="row flex">
                    <div className="col-xs-12 col-sm-6 ">
                        <a className="blog-prev-post" href="">
                            <span className="item-tag">Pre post</span>
                            <h4>Nay was appear entire ladies</h4>
                        </a>
                    </div>
                    <div className="col-xs-12 col-sm-6 ">
                        <a className="blog-next-post" href="">
                            <span className="item-tag">Next post</span>
                            <h4>Rabbit far rhinoceros cuffed gosh connected.</h4>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div> : <div></div>;

    return template;
}

const mapStateToProps = (state: RootState) => {
    return {
        blog: state.blogDetail
    }
}

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>) => {
    return bindActionCreators({
        getDetail: getBlog
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(BlogComponent);